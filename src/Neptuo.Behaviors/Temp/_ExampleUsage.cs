using Neptuo.Activators;
using Neptuo.Behaviors.Processing;
using Neptuo.Collections.Specialized;
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

    interface IQueryHandler<TQuery, TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }

    class QueryHandlerPipeline<T, TQuery, TResult> : IQueryHandler<TQuery, TResult>, IBehavior<T>
        where T : IQueryHandler<TQuery, TResult>
    {
        private readonly IPipeline<T> pipeline;
        private readonly IActivator<T> handlerFactory;

        public QueryHandlerPipeline(IPipeline<T> pipeline, IActivator<T> handlerFactory)
        {
            Ensure.NotNull(pipeline, "pipeline");
            Ensure.NotNull(handlerFactory, "handlerFactory");
            this.pipeline = pipeline.AddBehavior(PipelineBehaviorPosition.After, this);
            this.handlerFactory = handlerFactory;
        }

        async Task IBehavior<T>.ExecuteAsync(T handler, IBehaviorContext context)
        {
            TResult result = await handler.HandleAsync(context.CustomValues.Get<TQuery>("Query"));
            context.CustomValues.Add("Result", result);
        }

        public async Task<TResult> HandleAsync(TQuery query)
        {
            T instance = handlerFactory.Create();
            IKeyValueCollection customValues = new KeyValueCollection()
                .Add("Query", query);

            await pipeline.ExecuteAsync(instance, customValues);
            return customValues.Get<TResult>("Result");
        }
    }

}
