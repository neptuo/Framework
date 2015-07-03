using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neptuo.Activators
{
    /// <summary>
    /// Common extensions for <see cref="IDependencyProvider"/>.
    /// </summary>
    public static class _DependencyProviderExtensions
    {
        /// <summary>
        /// Resolves instance of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Required type.</typeparam>
        /// <returns>Instance of <typeparamref name="T"/>; if it's not possible to create instance.</returns>
        public static T Resolve<T>(this IDependencyProvider dependencyProvider)
        {
            Ensure.NotNull(dependencyProvider, "dependencyProvider");
            return (T)dependencyProvider.Resolve(typeof(T));
        }

        /// <summary>
        /// Resolves instance of <paramref name="requiredType"/> where <paramref name="requiredType"/> must be assignable to <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">Returned type.</typeparam>
        /// <param name="requiredType">Required type.</param>
        /// <returns>Instance of <paramref name="requiredType"/>; if it's not possible to create instance.</returns>
        public static T Resolve<T>(this IDependencyProvider dependencyProvider, Type requiredType)
        {
            Ensure.NotNull(dependencyProvider, "dependencyProvider");
            return (T)dependencyProvider.Resolve(requiredType);
        }
    }
}
