using Neptuo.Queries.Handlers;
using Neptuo.Queries.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            DefaultQueryHandlerDefinition<TQuery, TResult> definition = new DefaultQueryHandlerDefinition<TQuery, TResult>(handler, handler.HandleAsync);
            storage[typeof(TQuery)] = definition;
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

        public Task<TResult> QueryAsync<TQuery, TResult>(TQuery query)
            where TQuery : IQuery<TResult>
        {
            Ensure.NotNull(query, "query");

            Type queryType = query.GetType();
            DefaultQueryHandlerDefinition definition;
            if (storage.TryGetValue(queryType, out definition))
            {
                DefaultQueryHandlerDefinition<TQuery, TResult> target = (DefaultQueryHandlerDefinition<TQuery, TResult>)definition;
                return target.HandleAsync(query);
            }

            throw Ensure.Exception.ArgumentOutOfRange("query", "There isn't query handler for query of type '{0}'.", queryType.FullName);
        }
    }
}
