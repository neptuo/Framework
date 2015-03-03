using Microsoft.Practices.Unity;
using Neptuo.Activators.Internals;
using Neptuo.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators
{
    public class UnityDependencyContainer : DisposableBase, IDependencyContainer
    {
        private readonly ContainerScope scope;
        private readonly ScopeMapper scopeMapper;
        private readonly TargetMapper targetMapper;

        public UnityDependencyContainer()
            : this(new UnityContainer())
        { }

        public UnityDependencyContainer(IUnityContainer unityContainer)
            : this(new ContainerScope(unityContainer))
        { }

        private UnityDependencyContainer(ContainerScope scope)
        {
            this.scope = scope;
            this.scopeMapper = new ScopeMapper();
            this.targetMapper = new TargetMapper();
        }

        public IDependencyContainer AddMapping(Type requiredType, DependencyLifetime lifetime, object target)
        {
            LifetimeManager lifetimeManager = scopeMapper.CreateLifetimeManager(lifetime);
            targetMapper.Register(scope.UnityContainer, requiredType, lifetimeManager, target);
            return this;
        }

        public IDependencyContainer Scope(string name)
        {
            return new UnityDependencyContainer(scope.CreateScope(name));
        }

        public object Resolve(Type requiredType)
        {
            if (scope.UnityContainer.IsRegistered(requiredType))
                return scope.UnityContainer.Resolve(requiredType);

            throw new NotImplementedException();
        }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            scope.Dispose();
        }
    }
}
