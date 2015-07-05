using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators.Internals
{
    internal class DependencyDefinition : IDependencyDefinition
    {
        public Type RequiredType { get; private set; }
        public DependencyLifetime Lifetime { get; private set; }
        public ConstructorInfo ConstructorInfo { get; private set; }

        public object Target { get; private set; }
        public bool IsResolvable { get; private set; }

        public bool HasConstructorInfo
        {
            get { return ConstructorInfo != null; }
        }

        public string Key
        {
            get { return RequiredType.FullName; }
        }

        public DependencyDefinition(Type requiredType, DependencyLifetime lifetime, object target)
        {
            Ensure.NotNull(requiredType, "requiredType");
            Ensure.NotNull(target, "target");
            RequiredType = requiredType;
            Lifetime = lifetime;
        }

        public DependencyDefinition(Type requiredType, DependencyLifetime lifetime, object target, ConstructorInfo constructorInfo)
            : this(requiredType, lifetime, target)
        {
            Ensure.NotNull(constructorInfo, "constructorInfo");
            ConstructorInfo = constructorInfo;
        }
    }
}
