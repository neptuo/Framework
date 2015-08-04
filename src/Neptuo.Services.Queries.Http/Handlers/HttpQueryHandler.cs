using Neptuo.Services.HttpUtilities.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Queries.Handlers
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
        /// <param name="route">Route definition.</param>
        public HttpQueryHandler(RouteDefinition route)
        {
            this.dispatcher = new HttpQueryDispatcher(new DefaultRouteTable().Add(typeof(TQuery), route));
        }

        public Task<TResult> HandleAsync(TQuery query)
        {
            return dispatcher.QueryAsync(query);
        }
    }
}
