using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Net.Http.Clients.Routing
{
    /// <summary>
    /// Collection of input model routing.
    /// </summary>
    public interface IRouteTable
    {
        /// <summary>
        /// Tries to get <paramref name="route"/> for <paramref name="inputType"/>.
        /// </summary>
        /// <param name="inputType">Type of input object.</param>
        /// <param name="route">Route definition.</param>
        /// <returns><c>true</c> when such registration exits; <c>false</c> otherwise.</returns>
        bool TryGet(Type inputType, out RouteDefinition route);
    }
}
