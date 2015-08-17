using Neptuo.Activators;
using Neptuo.Behaviors.Processing;
using Neptuo.Behaviors.Processing.Reflection;
using Neptuo.Behaviors.Providers;
using Neptuo.Bootstrap.Behaviors;
using Neptuo.Bootstrap.Handlers;
using Neptuo.Bootstrap.Hierarchies.Sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Hierarchies
{
    public class Bootstrapper : IBootstrapper, IBootstrapTaskCollection
    {
        private readonly Dictionary<Type, IFactory<IBootstrapHandler>> storage = new Dictionary<Type, IFactory<IBootstrapHandler>>();
        private readonly List<Type> defaultDependencies = new List<Type>();
        private readonly ISortInputProvider inputProvider;
        private readonly ISortOutputProvider outputProvider;

        public Bootstrapper(ISortInputProvider inputProvider, ISortOutputProvider outputProvider)
        {
            Ensure.NotNull(inputProvider, "inputProvider");
            Ensure.NotNull(outputProvider, "outputProvider");
            this.inputProvider = inputProvider;
            this.outputProvider = outputProvider;
        }

        public IBootstrapTaskCollection Add<T>(IFactory<T> factory)
            where T : class, IBootstrapHandler
        {
            Ensure.NotNull(factory, "factory");
            storage[typeof(T)] = factory;
            return this;
        }

        public bool TryGet<T>(out IFactory<T> factory)
            where T : class, IBootstrapHandler
        {
            IFactory<IBootstrapHandler> innerFactory;
            if (storage.TryGetValue(typeof(T), out innerFactory))
            {
                factory = (IFactory<T>)innerFactory;
                return true;
            }

            factory = null;
            return true;
        }

        /// <summary>
        /// Adds <paramref name="type"/> to be known dependency by default.
        /// </summary>
        /// <param name="type">Known dependency.</param>
        /// <returns>Self (for fluency).</returns>
        public Bootstrapper AddDefaultDependency(Type type)
        {
            Ensure.NotNull(type, "type");
            defaultDependencies.Add(type);
            return this;
        }

        /// <summary>
        /// Adds <typeparamref name="T" /> to be known dependency by default.
        /// </summary>
        /// <typeparam name="T">Type of known dependency.</typeparam>
        /// <returns>Self (for fluency).</returns>
        public Bootstrapper AddDefaultDependency<T>()
        {
            return AddDefaultDependency(typeof(T));
        }

        public void Initialize()
        {
            // Sort tasks.
            IEnumerable<Type> sourceTypes = storage.Keys;
            Sorter sorter = new Sorter(inputProvider, outputProvider);
            IEnumerable<Type> targetTypes = sorter.Sort(sourceTypes, defaultDependencies);

            // Create instances (if needed).
            foreach (Type targetType in targetTypes)
            {
                IBootstrapHandler task = storage[targetType].Create();

                IPipeline<IBootstrapHandler> pipeline = new ReflectionPipeline<IBootstrapHandler>(new BehaviorProviderCollection(), new DefaultReflectionBehaviorFactory());
                pipeline.AddBehavior(PipelineBehaviorPosition.Before, new DependencyPropertyBehavior(null, null));
                pipeline.AddBehavior(PipelineBehaviorPosition.After, new InitializeBehavior());

                //pipeline.ExecuteAsync(task);
                //TODO: continue here.
            }
        }

        
    }
}
