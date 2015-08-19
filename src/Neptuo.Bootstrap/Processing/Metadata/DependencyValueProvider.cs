using Neptuo.Activators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Processing.Metadata
{
    public class DependencyValueProvider : IValueOutputProvider, IValueInputProvider
    {
        private readonly IDependencyContainer dependencyContainer;

        public DependencyValueProvider(IDependencyContainer dependencyContainer)
        {
            Ensure.NotNull(dependencyContainer, "dependencyContainer");
            this.dependencyContainer = dependencyContainer;
        }

        public void Export(Type dependencyType, object instance)
        {
            dependencyContainer.Definitions
                .Add(dependencyType, DependencyLifetime.NameScope(dependencyContainer.ScopeName), instance);
        }

        public object Import(Type depedencyType)
        {
            return dependencyContainer.Resolve(depedencyType);
        }
    }
}
