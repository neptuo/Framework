using Neptuo.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Creators.Building
{
    public class SimpleContainer : DisposableBase, IDependencyContainer
    {
        private readonly Dictionary<Type, IDependencyMapping> mappings = new Dictionary<Type, IDependencyMapping>();

        public IDependencyContainer RegisterMapping(Type requiredType, IDependencyMapping mapping)
        {
            throw new NotImplementedException();
        }

        public IDependencyContainer BeginScope(string name)
        {
            throw new NotImplementedException();
        }

        public object Resolve(Type requiredType, string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> ResolveAll(Type requiredType)
        {
            throw new NotImplementedException();
        }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
        }
    }
}
