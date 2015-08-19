using Neptuo.Behaviors.Processing;
using Neptuo.Behaviors.Processing.Reflection;
using Neptuo.Behaviors.Providers;
using Neptuo.Bootstrap.Handlers;
using Neptuo.Bootstrap.Handlers.Behaviors;
using Neptuo.Bootstrap.Processing.Metadata.Sorting;
using Neptuo.Bootstrap.Processing;
using Neptuo.Bootstrap.Processing.Behaviors;
using Neptuo.Bootstrap.Processing.Metadata;
using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neptuo.Bootstrap.Internals;

namespace Neptuo.Bootstrap
{
    /// <summary>
    /// Builder hierarchicall sorting bootstrappers.
    /// </summary>
    public class HierarchicalBuilder
    {
        internal ISortInputProvider SortInputProvider { get; set; }
        internal ISortOutputProvider SortOutputProvider { get; set; }
        internal IValueInputProvider ValueInputProvider { get; set; }
        internal IValueOutputProvider ValueOutputProvider { get; set; }
        internal List<Type> DefaultDependencies { get; set; }
        internal IBehaviorProvider BehaviorProvider { get; set; }
        internal IReflectionBehaviorFactory ReflectionBehaviorFactory { get; set; }

        /// <summary>
        /// Creates new instance of builder.
        /// </summary>
        public HierarchicalBuilder(IValueInputProvider inputProvider, IValueOutputProvider outputProvider)
        {
            Ensure.NotNull(inputProvider, "inputProvider");
            Ensure.NotNull(outputProvider, "outputProvider");
            SortInputProvider = new PropertySortProvider();
            SortOutputProvider = new PropertySortProvider();
            ValueInputProvider = inputProvider;
            ValueOutputProvider = outputProvider;
            DefaultDependencies = new List<Type>();
            ReflectionBehaviorFactory = new DefaultReflectionBehaviorFactory();
            BehaviorProvider = new BehaviorProviderCollection()
                .Add(
                    new AttributeBehaviorCollection()
                        .Add<IgnoreAutomaticAttribute, IBootstrapHandler, IgnoreAutomaticBehavior>()
                );
        }

        /// <summary>
        /// Adds handler input dependency sorting provider.
        /// </summary>
        /// <param name="inputProvider">Component providing handler inputs.</param>
        /// <returns>Self (for fluency).</returns>
        public HierarchicalBuilder AddSortInputProvider(ISortInputProvider inputProvider)
        {
            Ensure.NotNull(inputProvider, "inputProvider");
            SortInputProvider = inputProvider;
            return this;
        }

        /// <summary>
        /// Adds handler output (product) sorting provider.
        /// </summary>
        /// <param name="outputProvider">Component providing handler outputs.</param>
        /// <returns>Self (for fluency).</returns>
        public HierarchicalBuilder AddSortOutputProvider(ISortOutputProvider outputProvider)
        {
            Ensure.NotNull(outputProvider, "outputProvider");
            SortOutputProvider = outputProvider;
            return this;
        }

        /// <summary>
        /// Adds importer of dependencies.
        /// This some importer was also specified, <see cref="ValueInputCollection"/> is created <paramref name="inputProvider"/> is added into.
        /// </summary>
        /// <param name="inputProvider">Dependency importer to be used for importing handler inputs.</param>
        /// <returns>Self (for fluency).</returns>
        public HierarchicalBuilder AddDependencyImporter(IValueInputProvider inputProvider)
        {
            Ensure.NotNull(inputProvider, "inputProvider");
            if (ValueInputProvider != null)
            {
                ValueInputCollection composite = ValueInputProvider as ValueInputCollection;
                if (composite != null)
                {
                    composite.Add(inputProvider);
                }
                else
                {
                    ValueInputProvider = new ValueInputCollection()
                        .Add(inputProvider);
                }
            }
            else
            {
                ValueInputProvider = inputProvider;
            }

            return this;
        }

        /// <summary>
        /// Adds <paramref name="behaviorProvider"/> to be provider of handler behaviors.
        /// This some provider was also specified, <see cref="BehaviorProviderCollection"/> is created <paramref name="behaviorProvider"/> is added into.
        /// </summary>
        /// <param name="behaviorProvider">Handler behavior provider.</param>
        /// <returns>Self (for fluency).</returns>
        public HierarchicalBuilder AddBehaviorProvider(IBehaviorProvider behaviorProvider)
        {
            Ensure.NotNull(behaviorProvider, "behaviorProvider");
            if (BehaviorProvider != null)
            {
                BehaviorProviderCollection collection = behaviorProvider as BehaviorProviderCollection;
                if (collection != null)
                {
                    collection.Add(behaviorProvider);
                }
                else
                {
                    BehaviorProvider = new BehaviorProviderCollection()
                        .Add(behaviorProvider);
                }
            }
            else
            {
                BehaviorProvider = behaviorProvider;
            }

            return this;
        }

        /// <summary>
        /// Adds <paramref name="reflectionBehaviorFactory"/> to be provider of handler behavior instances.
        /// </summary>
        /// <param name="reflectionBehaviorFactory">Handler behavior instances provider.</param>
        /// <returns>Self (for fluency).</returns>
        public HierarchicalBuilder AddReflectionBehaviorFactory(IReflectionBehaviorFactory reflectionBehaviorFactory)
        {
            Ensure.NotNull(reflectionBehaviorFactory, "reflectionBehaviorFactory");
            ReflectionBehaviorFactory = reflectionBehaviorFactory;
            return this;
        }

        /// <summary>
        /// Adds <paramref name="type"/> to be known dependency by default.
        /// </summary>
        /// <param name="type">Known dependency.</param>
        /// <returns>Self (for fluency).</returns>
        public HierarchicalBuilder AddDefaultDependency(Type type)
        {
            Ensure.NotNull(type, "type");
            if (DefaultDependencies == null)
                DefaultDependencies = new List<Type>();

            DefaultDependencies.Add(type);
            return this;
        }

        /// <summary>
        /// Adds <typeparamref name="T" /> to be known dependency by default.
        /// </summary>
        /// <typeparam name="T">Type of known dependency.</typeparam>
        /// <returns>Self (for fluency).</returns>
        public HierarchicalBuilder AddDefaultDependency<T>()
        {
            return AddDefaultDependency(typeof(T));
        }

        /// <summary>
        /// Creates instance of manual handler loader.
        /// </summary>
        /// <returns>Configurated instance of <see cref="ManualLoaderBuilder"/>.</returns>
        public ManualLoaderBuilder ToManual()
        {
            return new ManualLoaderBuilder(
                new BootstrapHandlerExecutor(this),
                types => new Sorter(SortInputProvider, SortOutputProvider).Sort(types, DefaultDependencies)
            );
        }

        /// <summary>
        /// Creates instance of automatic handler loader.
        /// </summary>
        /// <returns>Configurated instance of <see cref="AutomaticLoaderBuilder"/>.</returns>
        public AutomaticLoaderBuilder ToAutomatic()
        {
            return new AutomaticLoaderBuilder(
                new BootstrapHandlerExecutor(this), 
                types => new Sorter(SortInputProvider, SortOutputProvider).Sort(types, DefaultDependencies)
            );
        }

        private class BootstrapHandlerExecutor : IBootstrapHandlerExecutor
        {
            private readonly HierarchicalBuilder builder;

            public BootstrapHandlerExecutor(HierarchicalBuilder builder)
            {
                this.builder = builder;
            }

            public void Execute(IBootstrapHandler handler)
            {
                IPipeline<IBootstrapHandler> pipeline = new ReflectionPipeline<IBootstrapHandler>(builder.BehaviorProvider, builder.ReflectionBehaviorFactory);
                pipeline.AddBehavior(PipelineBehaviorPosition.Before, new DependencyPropertyBehavior(builder.ValueInputProvider, builder.ValueOutputProvider));
                pipeline.AddBehavior(PipelineBehaviorPosition.After, new InitializeBehavior());

                Task task = pipeline.ExecuteAsync(handler, new KeyValueCollection().Add("IsManual", true));
                if (!task.IsCompleted && !task.IsCanceled)
                    task.RunSynchronously();
            }
        }
    }
}
