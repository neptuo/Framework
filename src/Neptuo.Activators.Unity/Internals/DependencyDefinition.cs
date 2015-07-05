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
    internal class DependencyDefinition : IDependencyDefinition
    {
        public Type RequiredType { get; private set; }
        public DependencyLifetime Lifetime { get; private set; }
        public object Target { get; private set; }
        public bool IsResolvable { get; private set; }

        public DependencyDefinition(Type requiredType, DependencyLifetime lifetime, object target, bool isResolvable = false)
        {
            Ensure.NotNull(requiredType, "requiredType");
            Ensure.NotNull(target, "target");
            RequiredType = requiredType;
            Lifetime = lifetime;
            Target = target;
            IsResolvable = isResolvable;
        }

        public IDependencyDefinition Clone(bool isResolvable)
        {
            return new DependencyDefinition(RequiredType, Lifetime, Target)
            {
                IsResolvable = isResolvable
            };
        }
    }
}
