using Neptuo.Behaviors.Providers;
using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Behaviors.Processing
{
    /// <summary>
    /// Base implementation of <see cref="IPipeline{T}"/>.
    /// Manages manually added behaviors, context creation, pipeline execution.
    /// The only need method to implement is <see cref="CreateBehaviorInstances"/> for creating instances of behaviors.
    /// </summary>
    /// <typeparam name="T">Type of inner handler.</typeparam>
    public abstract class PipelineBase<T> : IPipeline<T>
    {
        protected List<IBehavior<T>> PreBehaviors { get; private set; }
        protected List<IBehavior<T>> PostBehaviors { get; private set; }
        
        public IPipeline<T> AddBehavior(PipelineBehaviorPosition position, IBehavior<T> behavior)
        {
            Ensure.NotNull(behavior, "behavior");
            switch (position)
            {
                case PipelineBehaviorPosition.Before:
                    PreBehaviors.Add(behavior);
                    break;
                case PipelineBehaviorPosition.After:
                    PostBehaviors.Add(behavior);
                    break;
                default:
                    throw Ensure.Exception.NotSupported("Position '{0}' is not supported by ReflectionPipeline.", position);
            }

            return this;
        }

        /// <summary>
        /// Creates instance of <see cref="IBehaviorContext"/> for <paramref name="behaviors"/> 
        /// wrapping <paramref name="handler"/> with <paramref name="customValues"/> for created context.
        /// </summary>
        /// <param name="behaviors">Enumeration of behaviors.</param>
        /// <param name="handler">Instance of target handler.</param>
        /// <param name="customValues">Collection of context initial custom values.</param>
        /// <returns>Instance of <see cref="IBehaviorContext"/>.</returns>
        protected virtual IBehaviorContext CreateBehaviorContext(IEnumerable<IBehavior<T>> behaviors, T handler, IKeyValueCollection customValues)
        {
            return new DefaultBehaviorContext<T>(behaviors, handler, customValues);
        }

        /// <summary>
        /// Creates enumeration of behaviors.
        /// </summary>
        /// <returns>Enumeration of behaviors.</returns>
        protected virtual IEnumerable<IBehavior<T>> CreateBehaviors()
        {
            IEnumerable<IBehavior<T>> result = CreateBehaviorsInternal();
            if (PreBehaviors.Count > 0)
                result = Enumerable.Concat(PreBehaviors, result);

            if (PostBehaviors.Count > 0)
                result = Enumerable.Concat(result, PostBehaviors);

            return result;
        }

        /// <summary>
        /// Should provide behaviors, that are decorated with manually registered behaviors.
        /// </summary>
        /// <returns>Enumeration of behaviors.</returns>
        protected abstract IEnumerable<IBehavior<T>> CreateBehaviorsInternal();

        /// <summary>
        /// Using <see cref="CreateBehaviors"/>, behaviors are created. Then calling <see cref="CreateBehaviorContext"/> context is created. And then pipeline is executed.
        /// </summary>
        /// <param name="handler">Handler instance to execute pipeline on.</param>
        /// <param name="customValues">Collection of custom values passed around invokation.</param>
        /// <returns>Continuation task.</returns>
        public Task ExecuteAsync(T handler, IKeyValueCollection customValues)
        {
            IEnumerable<IBehavior<T>> behaviors = CreateBehaviors();
            IBehaviorContext context = CreateBehaviorContext(behaviors, handler, customValues);
            return context.NextAsync();
        }
    }
}
