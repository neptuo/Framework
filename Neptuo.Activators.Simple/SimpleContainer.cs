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
        private readonly IActivator<object, IDependencyActivatorContext> defaultActivator = new SimpleDefaultActivator();

        public IDependencyContainer AddMapping(Type requiredType, IActivator<object> activator)
        {
            throw new NotImplementedException();
        }

        public IDependencyContainer AddMapping(Type requiredType, IActivator<object, IDependencyActivatorContext> activator)
        {
            throw new NotImplementedException();
        }

        public IDependencyContainer BeginScope(string name)
        {
            throw new NotImplementedException();
        }

        public object TryResolve(Type requiredType, string name)
        {
            IActivator<object> activator;
            if (mappings.TryGetValue(requiredType, out activator))
                return activator.Create();

            IDependencyActivatorContext context = CreateContext(requiredType, name);
            IActivator<object, IDependencyActivatorContext> activatorWithContext;
            if (!mappingsWithContext.TryGetValue(requiredType, out activatorWithContext))
                activatorWithContext = defaultActivator;

            return activatorWithContext.Create(context);
        }

        private IDependencyActivatorContext CreateContext(Type requiredType, string name)
        {
            throw Guard.Exception.NotImplemented();
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
