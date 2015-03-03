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
        /// Registers mapping from <paramref name="requiredType"/> to <paramref name="target"/>
        /// </summary>
        /// <param name="requiredType">Required type.</param>
        /// <param name="lifetime">Lifetime of created instance.</param>
        /// <param name="target">Any supported target object.</param>
        /// <returns>Self (fluently).</returns>
        IDependencyContainer AddMapping(Type requiredType, DependencyLifetime lifetime, object target);
    }
}
