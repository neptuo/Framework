using Neptuo;
using Neptuo.Queries.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neptuo.Queries.Internals
{
    /// <summary>
    /// Basic definition of query handler.
    /// </summary>
    internal class DefaultQueryHandlerDefinition
    {
        /// <summary>
        /// Query handler.
        /// Should never be <c>null</c>.
        /// </summary>
        public object QueryHandler { get; }

        public DefaultQueryHandlerDefinition(object queryHandler)
        {
            QueryHandler = queryHandler;
        }
    }

    /// <summary>
    /// Output typed definition of query handler.
    /// Used when handling query from <see cref="IQueryDispatcher"/> for <see cref="IQuery{TOutput}"/>.
    /// </summary>
    /// <typeparam name="TQuery">A type of the query.</typeparam>
    /// <typeparam name="TResult">A type of the query result.</typeparam>
    internal class DefaultQueryHandlerDefinition<TQuery, TResult> : DefaultQueryHandlerDefinition, IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        public new IQueryHandler<TQuery, TResult> QueryHandler { get; }

        public DefaultQueryHandlerDefinition(IQueryHandler<TQuery, TResult> queryHandler)
            : base(queryHandler)
        {
            QueryHandler = queryHandler;
        }

        public Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken)
            => QueryHandler.HandleAsync(query, cancellationToken);
    }
}
