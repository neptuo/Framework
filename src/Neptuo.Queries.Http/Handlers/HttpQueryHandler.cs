using Neptuo;
using Neptuo.Net.Http.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neptuo.Queries.Handlers
{
    /// <summary>
    /// Implementation of <see cref="IQueryHandler{TQuery, TResult}"/> that transfers queries over HTTP.
    /// </summary>
    /// <typeparam name="TQuery">Type of query.</typeparam>
    /// <typeparam name="TResult">Type of result.</typeparam>
    public class HttpQueryHandler<TQuery, TResult> : IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        private readonly HttpQueryDispatcher dispatcher;

        /// <summary>
        /// Creates new instance that routes queries of type <typeparamref name="TQuery"/> to the <paramref name="route"/>.
        /// </summary>
        /// <param name="objectSender">An object sender.</param>
        public HttpQueryHandler(ObjectSender objectSender)
        {
            Ensure.NotNull(objectSender, "objectSender");
            dispatcher = new HttpQueryDispatcher(objectSender);
        }

        public Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken) => dispatcher.QueryAsync<TQuery, TResult>(query, cancellationToken);
    }
}
