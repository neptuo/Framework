using Neptuo.Activators.Building;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neptuo.Activators
{
    /// <summary>
    /// Service locator with ability to register services.
    /// </summary>
    public interface IDependencyContainer : IDependencyProvider
    {
        /// <summary>
        /// Registers mapping from <paramref name="requiredType"/> to <paramref name="activator"/>
        /// </summary>
        /// <param name="requiredType">Required type.</param>
        /// <param name="activator">Activator for providing instance.</param>
        /// <returns>Self (fluently).</returns>
        IDependencyContainer AddMapping(Type requiredType, IActivator<object, IDependencyActivatorContext> activator);

        /// <summary>
        /// Registers mapping from <paramref name="requiredType"/> to <paramref name="activator"/>
        /// </summary>
        /// <param name="requiredType">Required type.</param>
        /// <param name="activator">Activator for providing instance.</param>
        /// <returns>Self (fluently).</returns>
        IDependencyContainer AddMapping(Type requiredType, IActivator<object> activator);
    }
}
