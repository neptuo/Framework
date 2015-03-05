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

        private readonly string scopeName;
        
        private readonly IUnityContainer unityContainer;
        private readonly RegistrationMapper mapper;

        public UnityDependencyContainer()
            : this(new UnityContainer())
        { }

        public UnityDependencyContainer(IUnityContainer unityContainer)
            : this(RootScopeName, new RegistrationMapper(unityContainer, new MappingCollection(), RootScopeName), unityContainer)
        {
            Guard.NotNull(unityContainer, "unityContainer");
            this.unityContainer = unityContainer;
            this.scopeName = RootScopeName;
            this.mapper = new RegistrationMapper(unityContainer, new MappingCollection(), scopeName);
        }

        private UnityDependencyContainer(string scopeName, RegistrationMapper mapper, IUnityContainer unityContainer)
        {
            this.unityContainer = unityContainer;
            this.scopeName = scopeName;
            this.mapper = mapper;
            AddScopeMappings(mappings, scopeName);
        }

        private void AddScopeMappings(MappingCollection mappings, string scopeName)
        {
            IEnumerable<Mapping> scopeMappings;
            if (mappings.TryGet(scopeName, out scopeMappings))
            {
                foreach (Mapping mapping in scopeMappings)
                    AddMapping(mapping);
            }
        }

        public IDependencyContainer AddMapping(Type requiredType, DependencyLifetime lifetime, object target)
        {
            AddMapping(new Mapping(requiredType, lifetime, target));
            return this;
        }

        private void AddMapping(Mapping mapping)
        {
        }

        public IDependencyContainer Scope(string scopeName)
        {
            return new UnityDependencyContainer(
                scopeName, 
                new MappingCollection(mappings), 
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
