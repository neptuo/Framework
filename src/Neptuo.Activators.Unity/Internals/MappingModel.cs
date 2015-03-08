using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators.Internals
{
    internal class MappingModel
    {
        public Type RequiredType { get; private set; }
        public DependencyLifetime Lifetime { get; private set; }
        public object Target { get; private set; }

        public MappingModel(Type requiredType, DependencyLifetime lifetime, object target)
        {
            Ensure.NotNull(requiredType, "requiredType");
            Ensure.NotNull(target, "target");
            RequiredType = requiredType;
            Lifetime = lifetime;
            Target = target;
        }
    }
}
