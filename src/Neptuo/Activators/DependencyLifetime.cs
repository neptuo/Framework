using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators
{
    public struct DependencyLifetime
    {
        public readonly bool IsSingleton;
        public readonly bool IsTransient;
        public readonly bool IsScoped;
        public readonly bool IsNamed;
        public readonly string Name; 

        private DependencyLifetime(bool isSingleton, bool isTransient, bool isScoped, bool isNamed, string name)
        {
            IsSingleton = isSingleton;
            IsTransient = isTransient;
            IsScoped = isScoped;
            IsNamed = isNamed;
            Name = name;
        }

        public static readonly DependencyLifetime Singleton = new DependencyLifetime(true, false, false, false, null);
        public static readonly DependencyLifetime Transient = new DependencyLifetime(false, true, false, false, null);
        public static readonly DependencyLifetime AnyScope = new DependencyLifetime(false, false, true, false, null);
        
        public static DependencyLifetime NamedScope(string name)
        {
            return new DependencyLifetime(false, false, false, true, name);
        }
    }
}
