using Neptuo.Behaviors.Processing.Reflection;
using Neptuo.Behaviors.Providers;
using Neptuo.Bootstrap.Dependencies.Handlers;
using Neptuo.Bootstrap.Hierarchies.Sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Hierarchies
{
    /// <summary>
    /// Builder for <see cref="BootstrapperS"/>.
    /// Methods <see cref="BootstrapperBuilder.AddDependencyImporter"/> and <see cref="BootstrapperBuilder.AddDependencyExporter"/> must be called.
    /// Other components can be omitted.
    /// </summary>
    public class BootstrapperBuilder
    {
        private ISortInputProvider inputSorter;
        private ISortOutputProvider outputSorter;
        private IDependencyImporter dependencyImporter;
        private IDependencyExporter dependencyExporter;
        private List<Type> defaultDependencies;
        private IBehaviorProvider behaviorProvider;
        private IReflectionBehaviorFactory reflectionBehaviorFactory;

        /// <summary>
        /// Adds handler input dependency sorting provider.
        /// </summary>
        /// <param name="inputSorter">Component providing handler inputs.</param>
        /// <returns>Self (for fluency).</returns>
        public BootstrapperBuilder AddInputSorter(ISortInputProvider inputSorter)
        {
            Ensure.NotNull(inputSorter, "inputSorter");
            this.inputSorter = inputSorter;
            return this;
        }

        /// <summary>
        /// Adds handler output (product) sorting provider.
        /// </summary>
        /// <param name="outputSorter">Component providing handler outputs.</param>
        /// <returns>Self (for fluency).</returns>
        public BootstrapperBuilder AddOutputSorter(ISortOutputProvider outputSorter)
        {
            Ensure.NotNull(outputSorter, "outputSorter");
            this.outputSorter = outputSorter;
            return this;
        }

        /// <summary>
        /// Adds importer of dependencies.
        /// This some importer was also specified, <see cref="CompositeDependencyImporter"/> is created <paramref name="dependencyImporter"/> is added into.
        /// </summary>
        /// <param name="dependencyImporter">Dependency importer to be used for importing handler inputs.</param>
        /// <returns>Self (for fluency).</returns>
        public BootstrapperBuilder AddDependencyImporter(IDependencyImporter dependencyImporter)
        {
            Ensure.NotNull(dependencyImporter, "dependencyImporter");
            if (this.dependencyImporter != null)
            {
                CompositeDependencyImporter composite = this.dependencyImporter as CompositeDependencyImporter;
                if (composite != null)
                {
                    composite.Add(dependencyImporter);
                }
                else
                {
                    this.dependencyImporter = new CompositeDependencyImporter()
                        .Add(dependencyImporter);
                }
            }
            else
            {
                this.dependencyImporter = dependencyImporter;
            }

            return this;
        }

        /// <summary>
        /// Sets dependency exporter.
        /// </summary>
        /// <param name="dependencyExporter">Dependency exporter to be used for exporting handler outputs.</param>
        /// <returns>Self (for fluency).</returns>
        public BootstrapperBuilder AddDependencyExporter(IDependencyExporter dependencyExporter)
        {
            Ensure.NotNull(dependencyExporter, "dependencyExporter");
            this.dependencyExporter = dependencyExporter;
            return this;
        }

        /// <summary>
        /// Adds <paramref name="behaviorProvider"/> to be provider of handler behaviors.
        /// This some provider was also specified, <see cref="BehaviorProviderCollection"/> is created <paramref name="behaviorProvider"/> is added into.
        /// </summary>
        /// <param name="behaviorProvider">Handler behavior provider.</param>
        /// <returns>Self (for fluency).</returns>
        public BootstrapperBuilder AddBehaviorProvider(IBehaviorProvider behaviorProvider)
        {
            Ensure.NotNull(behaviorProvider, "behaviorProvider");
            if (this.behaviorProvider != null)
            {
                BehaviorProviderCollection collection = behaviorProvider as BehaviorProviderCollection;
                if (collection != null)
                {
                    collection.Add(behaviorProvider);
                }
                else
                {
                    this.behaviorProvider = new BehaviorProviderCollection()
                        .Add(behaviorProvider);
                }
            }
            else
            {
                this.behaviorProvider = behaviorProvider;
            }

            return this;
        }

        /// <summary>
        /// Adds <paramref name="reflectionBehaviorFactory"/> to be provider of handler behavior instances.
        /// </summary>
        /// <param name="reflectionBehaviorFactory">Handler behavior instances provider.</param>
        /// <returns>Self (for fluency).</returns>
        public BootstrapperBuilder AddReflectionBehaviorFactory(IReflectionBehaviorFactory reflectionBehaviorFactory)
        {
            Ensure.NotNull(reflectionBehaviorFactory, "reflectionBehaviorFactory");
            this.reflectionBehaviorFactory = reflectionBehaviorFactory;
            return this;
        }

        /// <summary>
        /// Adds <paramref name="type"/> to be known dependency by default.
        /// </summary>
        /// <param name="type">Known dependency.</param>
        /// <returns>Self (for fluency).</returns>
        public BootstrapperBuilder AddDefaultDependency(Type type)
        {
            Ensure.NotNull(type, "type");
            if (defaultDependencies == null)
                defaultDependencies = new List<Type>();

            defaultDependencies.Add(type);
            return this;
        }

        /// <summary>
        /// Adds <typeparamref name="T" /> to be known dependency by default.
        /// </summary>
        /// <typeparam name="T">Type of known dependency.</typeparam>
        /// <returns>Self (for fluency).</returns>
        public BootstrapperBuilder AddDefaultDependency<T>()
        {
            return AddDefaultDependency(typeof(T));
        }

        /// <summary>
        /// Creates instance of bootstrapper.
        /// </summary>
        /// <returns>Configurated instance of <see cref="Bootstrapper"/>.</returns>
        public Bootstrapper ToBootstrapper()
        {
            return new Bootstrapper(
                inputSorter ?? new PropertyImportExportProvider(),
                outputSorter ?? new PropertyImportExportProvider(),
                defaultDependencies ?? new List<Type>(),
                dependencyImporter,
                dependencyExporter,
                behaviorProvider ?? new BehaviorProviderCollection(),
                reflectionBehaviorFactory ?? new DefaultReflectionBehaviorFactory()
            );
        }
    }
}
