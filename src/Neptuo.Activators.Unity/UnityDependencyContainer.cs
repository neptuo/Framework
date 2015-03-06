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
        public const string RootScopeName = "Root";

        private readonly IUnityContainer unityContainer;
        private readonly RegistrationMapper mapper;

        public UnityDependencyContainer()
            : this(new UnityContainer())
        { }

        public UnityDependencyContainer(IUnityContainer unityContainer)
            : this(RootScopeName, new MappingCollection(), unityContainer)
        { }

        private UnityDependencyContainer(string scopeName, MappingCollection mappings, IUnityContainer unityContainer)
        {
            Guard.NotNull(unityContainer, "unityContainer");
            this.unityContainer = unityContainer;
            this.mapper = new RegistrationMapper(unityContainer, mappings, scopeName);
        }

        public IDependencyContainer Map(Type requiredType, DependencyLifetime lifetime, object target)
        {
            mapper.Map(new Mapping(requiredType, lifetime, target));
            return this;
        }

        public IDependencyContainer Scope(string scopeName)
        {
            return new UnityDependencyContainer(
                scopeName,
                mapper.Mappings, 
                unityContainer.CreateChildContainer()
            );
        }

        public object Resolve(Type requiredType)
        {
            return unityContainer.Resolve(requiredType);
        }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            unityContainer.Dispose();
        }
    }
}
