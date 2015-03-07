using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators.Internals
{
    internal class DependencyRegistryItem
    {
        public Type RequiredType { get; private set; }
        public DependencyLifetime Lifetime { get; private set; }
        public ConstructorInfo ConstructorInfo { get; private set; }

        public bool HasConstructorInfo
        {
            get { return ConstructorInfo != null; }
        }

        public DependencyRegistryItem(Type requiredType, DependencyLifetime lifetime)
        {
            Guard.NotNull(requiredType, "requiredType");
            RequiredType = requiredType;
            Lifetime = lifetime;
        }

        public DependencyRegistryItem(Type requiredType, DependencyLifetime lifetime, ConstructorInfo constructorInfo)
            : this(requiredType, lifetime)
        {
            Guard.NotNull(constructorInfo, "constructorInfo");
            ConstructorInfo = constructorInfo;
        }
    }
}
