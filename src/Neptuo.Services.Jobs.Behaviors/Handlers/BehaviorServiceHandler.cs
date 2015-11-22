using Neptuo.Activators;
using Neptuo.Behaviors;
using Neptuo.Behaviors.Processing;
using Neptuo.Jobs.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Jobs.Handlers
{
    /// <summary>
    /// Pipeline based implementation of <see cref="IBackgroundHandler"/>.
    /// </summary>
    /// <typeparam name="T">Type of inner handler.</typeparam>
    public class BehaviorServiceHandler<T> : DisposableBase, IBackgroundHandler, IBehavior<T>
        where T : IBackgroundHandler
    {
        private readonly IPipeline<T> pipeline;
        private readonly IFactory<T> handlerFactory;

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="pipeline">Behavior pipeline.</param>
        /// <param name="handlerFactory">Inner handler factory.</param>
        public BehaviorServiceHandler(IPipeline<T> pipeline, IFactory<T> handlerFactory)
        {
            Ensure.NotNull(pipeline, "pipeline");
            Ensure.NotNull(handlerFactory, "handlerFactory");
            this.pipeline = pipeline.AddBehavior(PipelineBehaviorPosition.After, this);
            this.handlerFactory = handlerFactory;
        }

        Task IBehavior<T>.ExecuteAsync(T handler, IBehaviorContext context)
        {
            handler.Invoke();
            return Task.FromResult(true);
        }

        public void Invoke()
        {
            T instance = handlerFactory.Create();
            pipeline.ExecuteAsync(instance);
        }
    }
}
