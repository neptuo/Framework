using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators.Internals
{
    internal class RegistrationMapper
    {
        private readonly IUnityContainer unityContainer;
        private readonly MappingCollection mappings;
        private readonly string scopeName;

        public RegistrationMapper(IUnityContainer unityContainer, MappingCollection mappings, string scopeName)
        {
            Guard.NotNull(unityContainer, "unityContainer");
            Guard.NotNull(mappings, "mappings");
            Guard.NotNull(scopeName, "scopeName");
            this.unityContainer = unityContainer;
            this.mappings = mappings;
            this.scopeName = scopeName;
        }

        public RegistrationMapper Map(Mapping model)
        {
            if (!model.Lifetime.IsNamed || model.Lifetime.Name == scopeName)
            {
                LifetimeManager lifetimeManager = CreateLifetimeManager(model.Lifetime);
                Register(unityContainer, model.RequiredType, lifetimeManager, model.Target);
            }

            if (!model.Lifetime.IsTransient)
                mappings.AddMapping(model);

            return this;
        }

        private LifetimeManager CreateLifetimeManager(DependencyLifetime lifetime)
        {
            if (lifetime.IsTransient)
                return new TransientLifetimeManager();

            if (lifetime.IsNamed)
                return new SingletonLifetimeManager();

            if (lifetime.IsScoped)
                return new HierarchicalLifetimeManager();

            // Not supported lifetime.
            throw Guard.Exception.NotSupported(lifetime.ToString());
        }

        private void Register(IUnityContainer unityContainer, Type requiredType, LifetimeManager lifetimeManager, object target)
        {
            Type targetType = target as Type;
            if (targetType != null)
            {
                unityContainer.RegisterType(requiredType, targetType, lifetimeManager);
                return;
            }

            //TODO: Implement using registered features...
            IActivator<object> targetActivator = target as IActivator<object>;
            if (targetActivator != null)
            {
                unityContainer.RegisterInstance(requiredType, targetActivator.Create());
                return;
            }
        }
    }
}
