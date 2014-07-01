using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo
{
    /// <summary>
    /// <see cref="INamedActivator"/> implemented as dependency provider resolver.
    /// </summary>
    /// <typeparam name="T">Type of service to resolve.</typeparam>
    public class DependencyNamedActivator<T> : INamedActivator<T>
    {
        private IDependencyProvider dependencyProvider;

        /// <summary>
        /// Creates new instance that uses <paramref name="dependencyProvider"/> as instance resolver.
        /// </summary>
        /// <param name="dependencyProvider">Instance resolver.</param>
        public DependencyNamedActivator(IDependencyProvider dependencyProvider)
        {
            Guard.NotNull(dependencyProvider, "dependencyProvider");
            this.dependencyProvider = dependencyProvider;
        }

        public T Create(string name)
        {
            return dependencyProvider.Resolve<T>(name);
        }
    }
}
