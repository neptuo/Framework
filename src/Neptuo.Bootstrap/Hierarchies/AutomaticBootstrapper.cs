using Neptuo.Activators;
using Neptuo.Behaviors.Processing;
using Neptuo.Behaviors.Processing.Reflection;
using Neptuo.Behaviors.Providers;
using Neptuo.Bootstrap.Behaviors;
using Neptuo.Bootstrap.Dependencies.Handlers;
using Neptuo.Bootstrap.Handlers;
using Neptuo.Bootstrap.Hierarchies.Sorting;
using Neptuo.Collections.Specialized;
using Neptuo.Reflection;
using Neptuo.Reflection.Enumerators.Executors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Hierarchies
{
    /// <summary>
    /// Hierarchical (input->output auto sorting) based implementation of <see cref="IBootstrapper"/> with automatic (scanning) handler registration.
    /// Supports only default contructor handlers.
    /// </summary>
    public class AutomaticBootstrapper : IBootstrapper
    {
        private readonly List<Type> defaultDependencies;
        private readonly ISortInputProvider inputProvider;
        private readonly ISortOutputProvider outputProvider;
        private readonly IDependencyImporter dependencyImporter;
        private readonly IDependencyExporter dependencyExporter;
        private readonly IBehaviorProvider behaviorProvider;
        private readonly IReflectionBehaviorFactory reflectionBehaviorFactory;

        internal AutomaticBootstrapper(ISortInputProvider inputProvider, ISortOutputProvider outputProvider, IEnumerable<Type> defaultDependencies, 
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

        public async Task Initialize()
        {
            // Get tasks.
            List<Type> sourceTypes = new List<Type>();
            using (ITypeExecutorService typeExecutors = ReflectionFactory.FromCurrentAppDomain().PrepareTypeExecutors())
            {
                typeExecutors
                    .AddFiltered(false)
                    .AddFilterNotAbstract()
                    .AddFilterNotInterface()
                    .AddFilterHasDefaultConstructor()
                    .AddFilter(t => typeof(IBootstrapHandler).IsAssignableFrom(t))
                    .AddHandler(sourceTypes.Add);
            }

            // Sort tasks.
            Sorter sorter = new Sorter(inputProvider, outputProvider);
            IEnumerable<Type> targetTypes = sorter.Sort(sourceTypes, defaultDependencies);

            // Create instances (if needed).
            foreach (Type targetType in targetTypes)
            {
                IBootstrapHandler handler = (IBootstrapHandler)Activator.CreateInstance(targetType);

                IPipeline<IBootstrapHandler> pipeline = new ReflectionPipeline<IBootstrapHandler>(behaviorProvider, reflectionBehaviorFactory);
                pipeline.AddBehavior(PipelineBehaviorPosition.Before, new DependencyPropertyBehavior(dependencyImporter, dependencyExporter));
                pipeline.AddBehavior(PipelineBehaviorPosition.After, new InitializeBehavior());

                await pipeline.ExecuteAsync(handler, new KeyValueCollection().Add("IsManual", true));
            }
        }
    }
}
