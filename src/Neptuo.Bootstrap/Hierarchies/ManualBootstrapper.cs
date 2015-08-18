using Neptuo.Activators;
using Neptuo.Behaviors.Processing;
using Neptuo.Behaviors.Processing.Reflection;
using Neptuo.Behaviors.Providers;
using Neptuo.Bootstrap.Behaviors;
using Neptuo.Bootstrap.Dependencies.Handlers;
using Neptuo.Bootstrap.Handlers;
using Neptuo.Bootstrap.Hierarchies.Sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Hierarchies
{
    /// <summary>
    /// Hierarchical (input->output auto sorting) based implementation of <see cref="IBootstrapper"/> with manual handler registration.
    /// </summary>
    public class ManualBootstrapper : IBootstrapper, IBootstrapTaskCollection
    {
        private readonly Dictionary<Type, IFactory<IBootstrapHandler>> storage = new Dictionary<Type, IFactory<IBootstrapHandler>>();
        private readonly List<Type> defaultDependencies;
        private readonly ISortInputProvider inputProvider;
        private readonly ISortOutputProvider outputProvider;
        private readonly IDependencyImporter dependencyImporter;
        private readonly IDependencyExporter dependencyExporter;
        private readonly IBehaviorProvider behaviorProvider;
        private readonly IReflectionBehaviorFactory reflectionBehaviorFactory;

        internal ManualBootstrapper(ISortInputProvider inputProvider, ISortOutputProvider outputProvider, IEnumerable<Type> defaultDependencies, 
            IDependencyImporter dependencyImporter, IDependencyExporter dependencyExporter, 
            IBehaviorProvider behaviorProvider, IReflectionBehaviorFactory reflectionBehaviorFactory)
        {
            Ensure.NotNull(inputProvider, "inputProvider");
            Ensure.NotNull(outputProvider, "outputProvider");
            Ensure.NotNull(defaultDependencies, "defaultDependencies");
            Ensure.NotNull(dependencyImporter, "dependencyImporter");
            Ensure.NotNull(dependencyExporter, "dependencyExporter");
            Ensure.NotNull(behaviorProvider, "behaviorProvider");
            Ensure.NotNull(reflectionBehaviorFactory, "reflectionBehaviorFactory");
            this.inputProvider = inputProvider;
            this.outputProvider = outputProvider;
            this.defaultDependencies = new List<Type>(defaultDependencies);
            this.dependencyImporter = dependencyImporter;
            this.dependencyExporter = dependencyExporter;
            this.behaviorProvider = behaviorProvider;
            this.reflectionBehaviorFactory = reflectionBehaviorFactory;
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
        public ManualBootstrapper AddDefaultDependency(Type type)
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
        public ManualBootstrapper AddDefaultDependency<T>()
        {
            return AddDefaultDependency(typeof(T));
        }

        public async Task Initialize()
        {
            // Sort tasks.
            IEnumerable<Type> sourceTypes = storage.Keys;
            Sorter sorter = new Sorter(inputProvider, outputProvider);
            IEnumerable<Type> targetTypes = sorter.Sort(sourceTypes, defaultDependencies);

            // Create instances (if needed).
            foreach (Type targetType in targetTypes)
            {
                IBootstrapHandler task = storage[targetType].Create();

                IPipeline<IBootstrapHandler> pipeline = new ReflectionPipeline<IBootstrapHandler>(behaviorProvider, reflectionBehaviorFactory);
                pipeline.AddBehavior(PipelineBehaviorPosition.Before, new DependencyPropertyBehavior(dependencyImporter, dependencyExporter));
                pipeline.AddBehavior(PipelineBehaviorPosition.After, new InitializeBehavior());

                await pipeline.ExecuteAsync(task);
            }
        }
    }
}
