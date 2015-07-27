using Neptuo.Activators;
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

    public interface IXPipeline<T>
    {
        Task ExecuteAsync(T instance, Action lastBehavior);
    }

    public class XBackgroundPipelineHandler<T> : IXBackgroundHandler
        where T : IXBackgroundHandler
    {
        private readonly IXPipeline<T> pipeline;
        private readonly IActivator<T> handlerFactory;

        public XBackgroundPipelineHandler(IXPipeline<T> pipeline, IActivator<T> handlerFactory)
        {
            Ensure.NotNull(pipeline, "pipeline");
            Ensure.NotNull(handlerFactory, "handlerFactory");
            this.pipeline = pipeline;
            this.handlerFactory = handlerFactory;
        }

        public void Invoke()
        {
            T instance = handlerFactory.Create();
            Task task = pipeline.ExecuteAsync(instance, () => instance.Invoke());
            if (!task.IsCompleted)
                task.RunSynchronously();
        }
    }
}
