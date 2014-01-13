using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ObjectBuilder.Client
{
    public class DependencyRegistryItem
    {
        public Type Target { get; set; }
        public IDependencyLifetime LifetimeManager { get; set; }
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
