using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neptuo.Creators
{
    /// <summary>
    /// Service locator with ability to register services.
    /// </summary>
    public interface IDependencyContainer : IDependencyProvider
    {
        /// <summary>
        /// Registers mapping from <paramref name="requiredType"/> to <paramref name="implementationType"/>
        /// </summary>
        /// <param name="requiredType">Required type.</param>
        /// <returns>This (fluently).</returns>
        IDependencyContainer RegisterMapping(Type requiredType, IDependencyMapping mapping);
    }
}
