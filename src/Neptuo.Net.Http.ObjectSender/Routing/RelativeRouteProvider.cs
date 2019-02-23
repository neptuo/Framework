 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Net.Http.Routing
{
    /// <summary>
    /// An implementation of a <see cref="IRouteProvider"/> which uses inner route provider and makes relative URLs absolute.
    /// 
    /// The inner provider can return absolute URL, then this class does nothing,
    /// but when the inner provider returns a relative URL (prefixed with the '~/'), the base url is applied.
    /// </summary>
    public class RelativeRouteProvider : IRouteProvider
    {
        private readonly string baseUrl;
        private readonly IRouteProvider routeTable;

        /// <summary>
        /// Creates a new instance with a <paramref name="baseUrl"/> and a inner <paramref name="routeTable"/>.
        /// </summary>
        /// <param name="baseUrl">A base URL prepended to relative URLs.</param>
        /// <param name="routeTable">An inner route provider.</param>
        public RelativeRouteProvider(string baseUrl, IRouteProvider routeTable)
        {
            Ensure.NotNullOrEmpty(baseUrl, "baseUrl");
            Ensure.NotNull(routeTable, "routeTable");

            if (!Uri.TryCreate(baseUrl, UriKind.Absolute, out Uri url))
                throw Ensure.Exception.ArgumentOutOfRange("baseUrl", "Base URL must be valid and absolute URL.");

            this.baseUrl = baseUrl;
            this.routeTable = routeTable;
        }

        public bool TryGet(Type inputType, out RouteDefinition route)
        {
            if (routeTable.TryGet(inputType, out route))
            {
                // If the URL in route is absolute, nothing is needed.
                if (Uri.TryCreate(route.Url, UriKind.Absolute, out Uri url))
                    return true;

                if (route.Url.StartsWith("~/"))
                {
                    string relativeUrl = route.Url.Substring(1);
                    route = new RouteDefinition(baseUrl + relativeUrl, route.Method, route.RequestSerializer, route.ResponseDeserializer, route.ContentType);
                    return true;
                }
            }

            route = null;
            return false;
        }
    }
}
