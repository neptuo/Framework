using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators.Internals
{
    /// <summary>
    /// Implementation of <see cref="IDependencyDefinition"/>.
    /// </summary>
    internal class UnityDependencyDefinition : IDependencyDefinition
    {
        public Type RequiredType { get; private set; }
        public DependencyLifetime Lifetime { get; private set; }
        public object Target { get; private set; }

        public UnityDependencyDefinition(Type serviceType, DependencyLifetime lifetime, object target)
        {
            Ensure.NotNull(serviceType, "requiredType");
            Ensure.NotNull(target, "target");
            RequiredType = serviceType;
            Lifetime = lifetime;
            Target = target;
        }
    }
}
