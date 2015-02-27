using Neptuo.Activators.Building;
using Neptuo.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators
{
    public class SimpleContainer : DisposableBase, IDependencyContainer
    {
        private readonly Dictionary<Type, IActivator<object>> mappings = new Dictionary<Type, IActivator<object>>();
        private readonly Dictionary<Type, IActivator<object, IDependencyActivatorContext>> mappingsWithContext = new Dictionary<Type, IActivator<object, IDependencyActivatorContext>>();

        public IDependencyContainer RegisterMapping(Type requiredType, IActivator<object> activator)
        {
            throw new NotImplementedException();
        }

        public IDependencyContainer RegisterMapping(Type requiredType, IActivator<object, IDependencyActivatorContext> activator)
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
