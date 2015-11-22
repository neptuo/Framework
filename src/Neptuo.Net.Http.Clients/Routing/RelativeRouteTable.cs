using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Net.Http.Clients.Routing
{
    /// <summary>
    /// Implementation of <see cref="IRouteTable"/> which uses inner route table
    /// and makes relative URLs absolute.
    /// 
    /// Inner table can return absolute URL, then this class does nothing,
    /// but when inner table return relative URL (prefixed with '~/'), base url is applied.
    /// </summary>
    public class RelativeRouteTable : IRouteTable
    {
        private readonly string baseUrl;
        private readonly IRouteTable routeTable;

        /// <summary>
        /// Creates new instance with <paramref name="baseUrl"/> and inner <paramref name="routeTable"/>.
        /// </summary>
        /// <param name="baseUrl">Base URL prepended to relative URLs.</param>
        /// <param name="routeTable">Inner route table.</param>
        public RelativeRouteTable(string baseUrl, IRouteTable routeTable)
        {
            Ensure.NotNullOrEmpty(baseUrl, "baseUrl");
            Ensure.NotNull(routeTable, "routeTable");

            Uri url;
            if (!Uri.TryCreate(baseUrl, UriKind.Absolute, out url))
                throw Ensure.Exception.ArgumentOutOfRange("baseUrl", "Base URL must be valid and absolute URL.");

            this.baseUrl = baseUrl;
            this.routeTable = routeTable;
        }

        public bool TryGet(Type inputType, out RouteDefinition route)
        {
            if (routeTable.TryGet(inputType, out route))
            {
                // If URL in route is absolute, nothing is needed.
                Uri url;
                if (Uri.TryCreate(route.Url, UriKind.Absolute, out url))
                    return true;

                if (route.Url.StartsWith("~/"))
                {
                    string relativeUrl = route.Url.Substring(1);
                    route = new RouteDefinition(baseUrl + relativeUrl, route.Method, route.Serialization);
                    return true;
                }
            }

            route = null;
            return false;
        }
    }
}
