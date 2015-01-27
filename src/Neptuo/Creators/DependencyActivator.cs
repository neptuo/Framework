using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Creators
{
    /// <summary>
    /// Implementation of <see cref="IActivator<T>"/> using <see cref="IDependencyProvider"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DependencyActivator<T> : IActivator<T>
    {
        private readonly IDependencyProvider dependencyProvider;

        public DependencyActivator(IDependencyProvider dependencyProvider)
        {
            Guard.NotNull(dependencyProvider, "dependencyProvider");
            this.dependencyProvider = dependencyProvider;
        }

        public T Create()
        {
            return dependencyProvider.Resolve<T>();
        }
    }
}
