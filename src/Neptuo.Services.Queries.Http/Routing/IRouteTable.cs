using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Queries.Routing
{
    /// <summary>
    /// Collection of query model routing.
    /// </summary>
    public interface IRouteTable
    {
        /// <summary>
        /// Tries to get <paramref name="route"/> for <paramref name="queryType"/>.
        /// </summary>
        /// <param name="queryType">Type of query object.</param>
        /// <param name="route">Route definition.</param>
        /// <returns><c>true</c> when such registration exits; <c>false</c> otherwise.</returns>
        bool TryGet(Type queryType, out RouteDefinition route);
    }
}
