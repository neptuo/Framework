using Neptuo.Exceptions.Helpers;
using Neptuo.Net.Http.Clients.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Net.Http.Clients
{
    /// <summary>
    /// Common extensions for <see cref="Ensure.Exception"/>.
    /// </summary>
    internal static class _EnsureExceptionExtensions
    {
        public static NotSupportedException NotSupportedSerialization(this EnsureExceptionHelper ensure, RouteSerialization serialization)
        {
            Ensure.NotNull(ensure, "ensure");
            return ensure.NotSupported("Not supported serialization type '{0}'.", serialization);
        }

        public static NotSupportedException NotSupportedMethod(this EnsureExceptionHelper ensure, RouteMethod method)
        {
            Ensure.NotNull(ensure, "ensure");
            return ensure.NotSupported("Not supported route method '{0}'.", method);
        }
    }
}
