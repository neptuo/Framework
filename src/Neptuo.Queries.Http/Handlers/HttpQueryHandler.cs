using Neptuo.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neptuo.Queries.Handlers
{
    /// <summary>
    /// An implementation of <see cref="IQueryHandler{TQuery, TResult}"/> that transfers queries over HTTP.
    /// </summary>
    /// <typeparam name="TQuery">A type of the query.</typeparam>
    /// <typeparam name="TResult">A type of the result.</typeparam>
    public class HttpQueryHandler<TQuery, TResult> : IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        private readonly HttpQueryDispatcher dispatcher;

        /// <summary>
        /// Creates a new instance that sends queries of type <typeparamref name="TQuery"/> throught <paramref name="objectSender"/>.
        /// </summary>
        /// <param name="objectSender">An object sender.</param>
        public HttpQueryHandler(ObjectSender objectSender) => dispatcher = new HttpQueryDispatcher(objectSender);

        public Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken) => dispatcher.QueryAsync<TQuery, TResult>(query, cancellationToken);
    }
}
