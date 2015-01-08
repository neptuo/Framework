using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands.Interception
{
    /// <summary>
    /// Provides access to interceptors defined for some command handler.
    /// </summary>
    public interface IInterceptorProvider
    {
        /// <summary>
        /// Returns list of interceptors registered from <paramref name="commandHandler"/>.
        /// </summary>
        /// <param name="commandHandler">Command handler.</param>
        /// <param name="command">Command that will be handled.</param>
        /// <param name="commandHandlerMethod">Method, that handles command on <paramref name="commandHandler"/>.</param>
        /// <returns>List of interceptors.</returns>
        IEnumerable<IDecoratedInvoke> GetInterceptors(object commandHandler, object command, MethodInfo commandHandlerMethod);
    }
}
