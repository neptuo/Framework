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
        public Type RequiredType { get; set; }
        public DependencyLifetime Lifetime { get; set; }
        public ConstructorInfo Constructor { get; set; }
        //public List<FactorySubRegistry> Properties { get; set; }
    }

    //internal class FactorySubRegistry
    //{
    //    public PropertyInfo Property { get; set; }
    //    public Type Target { get; set; }
    //    public string Name { get; set; }
    //}
}
