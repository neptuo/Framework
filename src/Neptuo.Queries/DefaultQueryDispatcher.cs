using Neptuo.Queries.Handlers;
using Neptuo.Queries.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neptuo.Queries
{
    /// <summary>
    /// A default implementation of a <see cref="IQueryDispatcher"/> and a <see cref="IQueryHandlerCollection"/>.
    /// When handling query and query handler is missing, exception is thrown.
    /// </summary>
    public class DefaultQueryDispatcher : IQueryHandlerCollection, IQueryDispatcher
    {
        private readonly Dictionary<Type, DefaultQueryHandlerDefinition> storage = new Dictionary<Type, DefaultQueryHandlerDefinition>();

        public IQueryHandlerCollection Add<TQuery, TResult>(IQueryHandler<TQuery, TResult> handler)
            where TQuery : IQuery<TResult>
        {
            Ensure.NotNull(handler, "handler");
            storage[typeof(TQuery)] = new DefaultQueryHandlerDefinition<TQuery, TResult>(handler);
            return this;
        }

        public bool TryGet<TQuery, TResult>(out IQueryHandler<TQuery, TResult> handler)
            where TQuery : IQuery<TResult>
        {
            DefaultQueryHandlerDefinition definition;
            if (storage.TryGetValue(typeof(TQuery), out definition))
            {
                handler = (IQueryHandler<TQuery, TResult>)definition.QueryHandler;
                return true;
            }

            handler = null;
            return false;
        }

        public Task<TResult> QueryAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default)
            where TQuery : IQuery<TResult>
        {
            Ensure.NotNull(query, "query");

            if (TryGet<TQuery, TResult>(out var handler))
                return handler.HandleAsync(query, cancellationToken);

            throw Ensure.Exception.ArgumentOutOfRange("query", $"There isn't a query handler for a query of the type '{query.GetType().FullName}'.");
        }
    }
}
