using Neptuo.Behaviors.Providers;
using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Behaviors.Processing.Reflection
{
    /// <summary>
    /// Implementation of <see cref="IPipeline{T}"/> based on runtime reflection info.
    /// </summary>
    /// <typeparam name="T">Type of handler.</typeparam>
    public class ReflectionPipeline<T> : PipelineBase<T>
    {
        private readonly IReflectionBehaviorFactory behaviorFactory;
        
        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="behaviorProvider">Behavior provider.</param>
        /// <param name="behaviorFactory">Factory for creating instances of behaviors.</param>
        public ReflectionPipeline(IBehaviorProvider behaviorProvider, IReflectionBehaviorFactory behaviorFactory)
            : base(behaviorProvider)
        {
            Ensure.NotNull(behaviorFactory, "behaviorFactory");
            this.behaviorFactory = behaviorFactory;
        }

        protected override IEnumerable<IBehavior<T>> CreateBehaviorInstances(IEnumerable<Type> behaviorTypes)
        {
            Type handlerType = typeof(T);
            IReflectionBehaviorFactoryContext context = new DefaultReflectionBehaviorFactoryContext(handlerType);
            IEnumerable<IBehavior<T>> result = behaviorTypes.Select(bt => (IBehavior<T>)behaviorFactory.TryCreate(context, bt));
            return result;
        }
    }
}
