using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators
{
    public struct DependencyLifetime
    {
        public readonly bool IsTransient;
        public readonly bool IsScoped;
        public readonly bool IsNamed;
        public readonly string Name; 

        private DependencyLifetime(bool isScoped, string name)
        {
            IsTransient = !isScoped;
            IsScoped = isScoped;
            IsNamed = !String.IsNullOrEmpty(name);
            Name = name;
        }

        public static readonly DependencyLifetime Transient = new DependencyLifetime(false, null);
        public static readonly DependencyLifetime AnyScope = new DependencyLifetime(true, null);
        
        public static DependencyLifetime NamedScope(string name)
        {
            return new DependencyLifetime(true, name);
        }
    }
}
