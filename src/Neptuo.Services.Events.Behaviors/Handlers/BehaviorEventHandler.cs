using Neptuo.Activators;
using Neptuo.Behaviors;
using Neptuo.Behaviors.Processing;
using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Events.Handlers
{
    /// <summary>
    /// Pipeline based implementation of event handler.
    /// </summary>
    /// <typeparam name="T">Type of inner handler.</typeparam>
    /// <typeparam name="TEvent">Type of event data.</typeparam>
    public class BehaviorEventHandler<T, TEvent> : IEventHandler<TEvent>, IBehavior<T>
        where T : IEventHandler<TEvent>
    {
        private readonly IPipeline<T> pipeline;
        private readonly IFactory<T> handlerFactory;

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="pipeline">Behavior pipeline.</param>
        /// <param name="handlerFactory">Inner handler factory.</param>
        public BehaviorEventHandler(IPipeline<T> pipeline, IFactory<T> handlerFactory)
        {
            Ensure.NotNull(pipeline, "pipeline");
            Ensure.NotNull(handlerFactory, "handlerFactory");
            this.pipeline = pipeline.AddBehavior(PipelineBehaviorPosition.After, this);
            this.handlerFactory = handlerFactory;
        }

        Task IBehavior<T>.ExecuteAsync(T handler, IBehaviorContext context)
        {
            return handler.HandleAsync(context.CustomValues.Get<TEvent>("Event"));
        }

        public Task HandleAsync(TEvent payload)
        {
            T instance = handlerFactory.Create();
            IKeyValueCollection customValues = new KeyValueCollection()
                .Add("Event", payload);

            return pipeline.ExecuteAsync(instance, customValues);
        }
    }
}
