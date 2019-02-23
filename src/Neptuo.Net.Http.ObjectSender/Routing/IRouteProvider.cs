using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Net.Http.Routing
{
    /// <summary>
    /// A provider of model routes.
    /// </summary>
    public interface IRouteProvider
    {
        /// <summary>
        /// Tries to get a <paramref name="route"/> for a <paramref name="inputType"/>.
        /// </summary>
        /// <param name="inputType">A type of input object.</param>
        /// <param name="route">A route definition.</param>
        /// <returns><c>true</c> when registration exits; <c>false</c> otherwise.</returns>
        bool TryGet(Type inputType, out RouteDefinition route);
    }
}
