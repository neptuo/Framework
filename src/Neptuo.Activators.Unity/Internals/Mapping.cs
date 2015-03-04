using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators.Internals
{
    internal class Mapping
    {
        public Type RequiredType { get; private set; }
        public DependencyLifetime Lifetime { get; private set; }
        public object Target { get; private set; }

        public Mapping(Type requiredType, DependencyLifetime lifetime, object target)
        {
            Guard.NotNull(requiredType, "requiredType");
            Guard.NotNull(target, "target");
            RequiredType = requiredType;
            Lifetime = lifetime;
            Target = target;
        }
    }
}
