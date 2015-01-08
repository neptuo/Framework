using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neptuo
{
    /// <summary>
    /// Service locator with ability to register services.
    /// </summary>
    public interface IDependencyContainer : IDependencyProvider
    {
        /// <summary>
        /// Registers singleton instance to this service locator.
        /// </summary>
        /// <param name="requiredType">Required type.</param>
        /// <param name="name">Optional type.</param>
        /// <param name="instance">Instance of <paramref name="requiredType"/>.</param>
        /// <returns>This (fluently).</returns>
        IDependencyContainer RegisterInstance(Type requiredType, string name, object instance);

        /// <summary>
        /// Registers mapping from <paramref name="requiredType"/> to <paramref name="implementationType"/>
        /// </summary>
        /// <param name="requiredType">Required type.</param>
        /// <param name="implementationType">Implementation type.</param>
        /// <param name="name">Optional type.</param>
        /// <param name="lifetime">Instance life time.</param>
        /// <returns>This (fluently).</returns>
        IDependencyContainer RegisterType(Type requiredType, Type implementationType, string name, object lifetime);
    }
}
