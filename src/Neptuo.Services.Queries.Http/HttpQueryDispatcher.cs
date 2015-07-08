using Neptuo.Services.Queries.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Queries
{
    public class HttpQueryDispatcher : IQueryDispatcher
    {
        private readonly IRouteTable routeTable;

        public HttpQueryDispatcher(IRouteTable routeTable)
        {
            Ensure.NotNull(routeTable, "routeTable");
            this.routeTable = routeTable;
        }

        public Task<TOutput> QueryAsync<TOutput>(IQuery<TOutput> query)
        {
            Ensure.NotNull(query, "query");
            Type queryType = query.GetType();
            RouteDefinition route;
            if (routeTable.TryGet(queryType, out route))
            {
                HttpClient httpClient = new HttpClient();
            }

            throw new NotImplementedException();
        }
    }
}
