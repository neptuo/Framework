using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo
{
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
