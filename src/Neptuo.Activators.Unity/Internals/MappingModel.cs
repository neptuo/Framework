using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators.Internals
{
    internal class MappingModel : IDependencyDefinition
    {
        public Type RequiredType { get; private set; }
        public DependencyLifetime Lifetime { get; private set; }
        public object Target { get; private set; }

        public MappingModel(Type serviceType, DependencyLifetime lifetime, object target)
        {
            Ensure.NotNull(serviceType, "requiredType");
            Ensure.NotNull(target, "target");
            RequiredType = serviceType;
            Lifetime = lifetime;
            Target = target;
        }
    }
}
