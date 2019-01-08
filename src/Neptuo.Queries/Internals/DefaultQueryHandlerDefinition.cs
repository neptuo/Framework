using Neptuo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public object QueryHandler { get; set; }

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
    internal class DefaultQueryHandlerDefinition<TQuery, TResult> : DefaultQueryHandlerDefinition
        where TQuery : IQuery<TResult>
    {
        public Func<TQuery, Task<TResult>> HandleMethod { get; set; }

        public DefaultQueryHandlerDefinition(object queryHandler, Func<TQuery, Task<TResult>> handleMethod)
            : base(queryHandler)
        {
            HandleMethod = handleMethod;
        }

        public Task<TResult> HandleAsync(TQuery query)
            => HandleMethod(query);
    }
}
