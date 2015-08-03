using Neptuo.Activators;
using Neptuo.Behaviors;
using Neptuo.Behaviors.Processing;
using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Commands.Handlers
{
    /// <summary>
    /// Pipeline based implementation of query handler.
    /// </summary>
    public class BehaviorCommandHandler<T, TCommand> : ICommandHandler<TCommand>, IBehavior<T>
        where T : ICommandHandler<TCommand>
    {
        private readonly IPipeline<T> pipeline;
        private readonly IActivator<T> handlerFactory;

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="pipeline">Behavior pipeline.</param>
        /// <param name="handlerFactory">Inner handler factory.</param>
        public BehaviorCommandHandler(IPipeline<T> pipeline, IActivator<T> handlerFactory)
        {
            Ensure.NotNull(pipeline, "pipeline");
            Ensure.NotNull(handlerFactory, "handlerFactory");
            this.pipeline = pipeline.AddBehavior(PipelineBehaviorPosition.After, this);
            this.handlerFactory = handlerFactory;
        }

        Task IBehavior<TCommand>.ExecuteAsync(T handler, IBehaviorContext context)
        {
            return handler.HandleAsync(context.CustomValues.Get<TCommand>("Command"));
        }

        public Task HandleAsync(TCommand command)
        {
            T instance = handlerFactory.Create();
            IKeyValueCollection customValues = new KeyValueCollection()
                .Set("Command", command);

            return pipeline.ExecuteAsync(instance, customValues);
        }
    }
}
