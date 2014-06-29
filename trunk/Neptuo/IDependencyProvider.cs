using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neptuo
{
    /// <summary>
    /// Service locator.
    /// </summary>
    public interface IDependencyProvider
    {
        /// <summary>
        /// Creates new child container based on this provider.
        /// </summary>
        /// <returns>New child container based on this provider.</returns>
        IDependencyContainer CreateChildContainer();

        /// <summary>
        /// Resolves instance of <paramref name="requiredType"/>.
        /// </summary>
        /// <param name="requiredType">Required type.</param>
        /// <param name="name">Optional type.</param>
        /// <returns>Instance of <paramref name="requiredType"/>.</returns>
        object Resolve(Type requiredType, string name);

        /// <summary>
        /// Resolves all instances of <paramref name="requiredType"/>.
        /// </summary>
        /// <param name="requiredType">Required type.</param>
        /// <returns>All instances of <paramref name="requiredType"/>.</returns>
        IEnumerable<object> ResolveAll(Type requiredType);
    }
}
