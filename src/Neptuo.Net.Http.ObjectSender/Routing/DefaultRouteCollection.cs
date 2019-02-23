using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Net.Http.Routing
{
    /// <summary>
    /// A default (with manual registration) implementation of the <see cref="IRouteProvider"/>.
    /// </summary>
    public class DefaultRouteCollection : IRouteProvider
    {
        private readonly Dictionary<Type, RouteDefinition> storage = new Dictionary<Type, RouteDefinition>();

        /// <summary>
        /// Adds a mapping for a <paramref name="inputType"/> to a <paramref name="route"/>.
        /// </summary>
        /// <param name="inputType">A type of the input object.</param>
        /// <param name="route">A route definition.</param>
        /// <returns>Self (for fluency).</returns>
        public DefaultRouteCollection Add(Type inputType, RouteDefinition route)
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
