using Neptuo.Activators;
using Neptuo.Behaviors;
using Neptuo.Behaviors.Processing;
using Neptuo.Collections.Specialized;
using Neptuo.Services.Queries.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Queries.Behaviors
{
    /// <summary>
    /// Pipeline based implementatio of query handler.
    /// </summary>
    /// <typeparam name="T">Type of inner handler.</typeparam>
    /// <typeparam name="TQuery">Type of query parameters.</typeparam>
    /// <typeparam name="TResult">Type of query result.</typeparam>
    public class QueryHandler<T, TQuery, TResult> : IQueryHandler<TQuery, TResult>, IBehavior<T>
        where TQuery : IQuery<TResult>
        where T : IQueryHandler<TQuery, TResult>
    {
        private readonly IPipeline<T> pipeline;
        private readonly IActivator<T> handlerFactory;

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="pipeline">Behavior pipeline.</param>
        /// <param name="handlerFactory">Inner handler factory.</param>
        public QueryHandler(IPipeline<T> pipeline, IActivator<T> handlerFactory)
        {
            Ensure.NotNull(pipeline, "pipeline");
            Ensure.NotNull(handlerFactory, "handlerFactory");
            this.pipeline = pipeline.AddBehavior(PipelineBehaviorPosition.After, this);
            this.handlerFactory = handlerFactory;
        }

        async Task IBehavior<T>.ExecuteAsync(T handler, IBehaviorContext context)
        {
            TResult result = await handler.HandleAsync(context.CustomValues.Get<TQuery>("Query"));
            context.CustomValues.Set("Result", result);
        }

        public async Task<TResult> HandleAsync(TQuery query)
        {
            T instance = handlerFactory.Create();
            IKeyValueCollection customValues = new KeyValueCollection()
                .Set("Query", query);

            await pipeline.ExecuteAsync(instance, customValues);
            return customValues.Get<TResult>("Result");
        }
    }
}
