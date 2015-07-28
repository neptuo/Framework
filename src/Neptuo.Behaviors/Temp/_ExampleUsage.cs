using Neptuo.Activators;
using Neptuo.Behaviors.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Behaviors
{
    public interface IXBackgroundHandler
    {
        void Invoke();
    }

    public class XBackgroundPipelineHandler<T> : IXBackgroundHandler, IBehavior<T>
        where T : IXBackgroundHandler
    {
        private readonly IPipeline<T> pipeline;
        private readonly IActivator<T> handlerFactory;

        public XBackgroundPipelineHandler(IPipeline<T> pipeline, IActivator<T> handlerFactory)
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
            Task task = pipeline.ExecuteAsync(instance);
            if (!task.IsCompleted)
                task.RunSynchronously();
        }
    }
}
