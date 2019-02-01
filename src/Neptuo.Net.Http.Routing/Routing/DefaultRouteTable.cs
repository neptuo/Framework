using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Net.Http.Routing
{
    /// <summary>
    /// Default (with manual registration) implementation of <see cref="IRouteTable"/>.
    /// </summary>
    public class DefaultRouteTable : IRouteTable
    {
        private readonly Dictionary<Type, RouteDefinition> storage = new Dictionary<Type, RouteDefinition>();

        /// <summary>
        /// Add mapping for <paramref name="inputType"/> to <paramref name="route"/>.
        /// </summary>
        /// <param name="inputType">Type of input object.</param>
        /// <param name="route">Route definition.</param>
        /// <returns>Self (for fluency).</returns>
        public DefaultRouteTable Add(Type inputType, RouteDefinition route)
        {
            Ensure.NotNull(inputType, "inputType");
            Ensure.NotNull(route, "route");
            storage[inputType] = route;
            return this;
        }

        public bool TryGet(Type inputType, out RouteDefinition route)
        {
            Ensure.NotNull(inputType, "inputType");
            return storage.TryGetValue(inputType, out route);
        }
    }
}
