using Neptuo.Behaviors.Providers;
using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Behaviors.Processing.Reflection
{
    public class ReflectionPipeline<T> : IPipeline<T>
    {
        private readonly IBehaviorProvider behaviorProvider;
        private readonly IReflectionBehaviorFactory behaviorFactory;
        private readonly List<IBehavior<T>> preBehaviors = new List<IBehavior<T>>();
        private readonly List<IBehavior<T>> postBehaviors = new List<IBehavior<T>>();

        public ReflectionPipeline(IBehaviorProvider behaviorProvider, IReflectionBehaviorFactory behaviorFactory)
        {
            Ensure.NotNull(behaviorProvider, "behaviorProvider");
            Ensure.NotNull(behaviorFactory, "behaviorFactory");
            this.behaviorProvider = behaviorProvider;
            this.behaviorFactory = behaviorFactory;
        }

        public IPipeline<T> AddBehavior(PipelineBehaviorPosition position, IBehavior<T> behavior)
        {
            Ensure.NotNull(behavior, "behavior");
            switch (position)
            {
                case PipelineBehaviorPosition.Before:
                    preBehaviors.Add(behavior);
                    break;
                case PipelineBehaviorPosition.After:
                    postBehaviors.Add(behavior);
                    break;
                default:
                    throw Ensure.Exception.NotSupported("Position '{0}' is not supported by ReflectionPipeline.", position);
            }

            return this;
        }

        protected virtual IBehaviorContext CreateBehaviorContext(IEnumerable<IBehavior<T>> behaviors, T handler, IKeyValueCollection customValues)
        {
            return new DefaultBehaviorContext<T>(behaviors, handler, customValues);
        }

        protected virtual IEnumerable<IBehavior<T>> CreateBehaviors()
        {
            Type handlerType = typeof(T);
            IReflectionContext context = new DefaultReflectionContext(handlerType);
            IEnumerable<IBehavior<T>> result = behaviorProvider
                .GetBehaviors(handlerType)
                .Select(bt => (IBehavior<T>)behaviorFactory.TryCreate(context, bt));

            if (preBehaviors.Count > 0)
                result = Enumerable.Concat(preBehaviors, result);

            if (postBehaviors.Count > 0)
                result = Enumerable.Concat(result, postBehaviors);

            return result;
        }

        public Task ExecuteAsync(T handler, IKeyValueCollection customValues)
        {
            IEnumerable<IBehavior<T>> behaviors = CreateBehaviors();
            IBehaviorContext context = CreateBehaviorContext(behaviors, handler, customValues);
            return context.NextAsync();
        }
    }
}
