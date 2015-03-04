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
        private readonly Dictionary<Type, IActivator<object, IDependencyContext>> mappingsWithContext = new Dictionary<Type, IActivator<object, IDependencyContext>>();
        private readonly IActivator<object, IDependencyContext> defaultActivator = new SimpleDefaultActivator();

        public IDependencyContainer AddMapping(Type requiredType, DependencyLifetime lifetime, object target)
        {
            throw new NotImplementedException();
        }

        public IDependencyContainer Scope(string name)
        {
            throw new NotImplementedException();
        }

        public object Resolve(Type requiredType)
        {
            IActivator<object> activator;
            if (mappings.TryGetValue(requiredType, out activator))
                return activator.Create();

            IDependencyContext context = CreateContext(requiredType);
            IActivator<object, IDependencyContext> activatorWithContext;
            if (!mappingsWithContext.TryGetValue(requiredType, out activatorWithContext))
                activatorWithContext = defaultActivator;

            return activatorWithContext.Create(context);
        }

        private IDependencyContext CreateContext(Type requiredType)
        {
            throw Guard.Exception.NotImplemented();
        }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
        }
    }
}
