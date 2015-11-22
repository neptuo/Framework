using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Net.Http.Clients.Routing
{
    /// <summary>
    /// Defines route.
    /// </summary>
    public class RouteDefinition
    {
        /// <summary>
        /// Route URL.
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// HTTP method used for the route.
        /// </summary>
        public RouteMethod Method { get; private set; }

        /// <summary>
        /// Serialization format used for the route.
        /// </summary>
        public RouteSerialization Serialization { get; private set; }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="url">Route URL.</param>
        /// <param name="method">HTTP method used for the route.</param>
        /// <param name="serialization">Serialization format used for the route.</param>
        public RouteDefinition(string url, RouteMethod method = RouteMethod.Get, RouteSerialization serialization = RouteSerialization.Json)
        {
            Url = url;
            Method = method;
            Serialization = serialization;
        }
    }
}
