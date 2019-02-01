using Neptuo.Net.Http.Routing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neptuo.Queries
{
    /// <summary>
    /// Implementation of <see cref="IQueryDispatcher"/> that transfers queries over HTTP.
    /// </summary>
    public class HttpQueryDispatcher : IQueryDispatcher
    {
        private readonly IRouteTable routeTable;
        private readonly HttpClientAdapter httpAdapter;

        /// <summary>
        /// Creates new instance with absolute URLs defined in <paramref name="routeTable"/>.
        /// </summary>
        /// <param name="routeTable">Route table with absolute URLs.</param>
        public HttpQueryDispatcher(IRouteTable routeTable)
        {
            Ensure.NotNull(routeTable, "routeTable");
            this.routeTable = routeTable;
            this.httpAdapter = new HttpClientAdapter(routeTable);
        }

        public async Task<TResult> QueryAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default)
            where TQuery : IQuery<TResult>
        {
            Ensure.NotNull(query, "query");
            Type queryType = query.GetType();
            RouteDefinition route;
            if (routeTable.TryGet(queryType, out route))
            {
                using (HttpClient httpClient = httpAdapter.PrepareHttpClient(route))
                {
                    // Prepare content and send request.
                    ObjectContent objectContent = httpAdapter.PrepareObjectContent(query, route);
                    HttpResponseMessage response = await httpAdapter.Execute(httpClient, objectContent, route);

                    // Parse output.
                    TResult output = await response.Content.ReadAsAsync<TOutput>();
                    return output;
                }
            }

            throw Ensure.Exception.InvalidOperation("Unnable to preces query without registered URL route, query type is '{0}'.", queryType.FullName);
        }
    }
}
