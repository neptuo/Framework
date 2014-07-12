using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Routing.Conventions
{
    /// <summary>
    /// Decides whether can be routed and creates route for it.
    /// </summary>
    public interface IConvention
    {
        /// <summary>
        /// Tries to create route from <paramref name="typeDefinition"/>.
        /// If route can't be created, returns <c>false</c> and sets <paramref name="route"/> to <c>null</c>.
        /// </summary>
        /// <param name="typeDefinition">Possible handler type.</param>
        /// <param name="route">Route for <paramref name="typeDefinition"/>.</param>
        /// <returns>If route can't be created, returns <c>false</c> and sets <paramref name="route"/> to <c>null</c>.</returns>
        bool TryGetRoute(Type typeDefinition, out IRoute route);
    }
}
