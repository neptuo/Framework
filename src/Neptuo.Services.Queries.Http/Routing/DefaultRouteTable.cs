using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Queries.Routing
{
    /// <summary>
    /// Default (with manual registration) implementation of <see cref="IRouteTable"/>.
    /// </summary>
    public class DefaultRouteTable : IRouteTable
    {
        private readonly Dictionary<Type, RouteDefinition> storage = new Dictionary<Type, RouteDefinition>();

        /// <summary>
        /// Add mapping for <paramref name="queryType"/> to <paramref name="route"/>.
        /// </summary>
        /// <param name="queryType">Type of query object.</param>
        /// <param name="route">Route definition.</param>
        /// <returns>Self (for fluency).</returns>
        public DefaultRouteTable Add(Type queryType, RouteDefinition route)
        {
            Ensure.NotNull(queryType, "queryType");
            Ensure.NotNull(route, "route");
            storage[queryType] = route;
            return this;
        }

        public bool TryGet(Type queryType, out RouteDefinition route)
        {
            Ensure.NotNull(queryType, "queryType");
            return storage.TryGetValue(queryType, out route);
        }
    }
}
