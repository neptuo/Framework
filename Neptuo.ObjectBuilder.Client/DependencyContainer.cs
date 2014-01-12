using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ObjectBuilder.Client
{
    public class DependencyContainer : IDependencyContainer
    {
        public IDependencyContainer RegisterInstance(Type t, string name, object instance)
        {
            throw new NotImplementedException();
        }

        public IDependencyContainer RegisterType(Type from, Type to, string name, object lifetime)
        {
            throw new NotImplementedException();
        }

        public IDependencyContainer CreateChildContainer()
        {
            throw new NotImplementedException();
        }

        public object Resolve(Type t, string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> ResolveAll(Type t)
        {
            throw new NotImplementedException();
        }
    }
}
