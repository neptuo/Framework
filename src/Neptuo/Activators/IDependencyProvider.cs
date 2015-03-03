using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neptuo.Activators
{
    /// <summary>
    /// Service locator with hierarchy support.
    /// </summary>
    public interface IDependencyProvider : IDisposable
    {
        /// <summary>
        /// Creates new child container based on this provider.
        /// </summary>
        /// <param name="name">Optional name for named scopes.</param>
        /// <returns>New child container based on this provider.</returns>
        IDependencyContainer Scope(string name);

        /// <summary>
        /// Resolves instance of <paramref name="requiredType"/>.
        /// </summary>
        /// <param name="requiredType">Required type.</param>
        /// <param name="name">Optional name.</param>
        /// <returns>Instance of <paramref name="requiredType"/>; if it's not possible to create instance, return <c>null</c>.</returns>
        object Resolve(Type requiredType);
    }
}
