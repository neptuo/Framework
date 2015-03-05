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
        private readonly MappingCollection mappings;
        private readonly IUnityContainer unityContainer;
        private readonly UnityDependencyContainer parentContainer;
        private readonly ScopeMapper scopeMapper;
        private readonly TargetMapper targetMapper;

        public UnityDependencyContainer()
            : this(new UnityContainer())
        { }

        public UnityDependencyContainer(IUnityContainer unityContainer)
        {
            Guard.NotNull(unityContainer, "unityContainer");
            this.unityContainer = unityContainer;
            this.scopeName = RootScopeName;
            this.mappings = new MappingCollection();
            this.scopeMapper = new ScopeMapper();
            this.targetMapper = new TargetMapper();
        }

        private UnityDependencyContainer(string scopeName, MappingCollection parentMappings, IUnityContainer unityContainer)
            : this()
        {
            this.scopeName = scopeName;
            this.mappings = new MappingCollection(parentMappings);
			this.unityContainer = unityContainer;
            //this.parentContainer = parentContainer;
            //AddScopeMappings(parentMappings, String.Empty);
            AddScopeMappings(parentMappings, scopeName);
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
            if (!mapping.Lifetime.IsNamed || mapping.Lifetime.Name == scopeName)
            {
                LifetimeManager lifetimeManager = scopeMapper.CreateLifetimeManager(mapping.Lifetime);
                targetMapper.Register(unityContainer, mapping.RequiredType, lifetimeManager, mapping.Target);
            }

            if (!mapping.Lifetime.IsTransient)
                mappings.AddMapping(mapping);
        }

        public IDependencyContainer Scope(string name)
        {
            return new UnityDependencyContainer(name, mappings, unityContainer.CreateChildContainer());
        }

        public object Resolve(Type requiredType)
        {
            // Try to resolve by this container.
		    if (unityContainer.IsRegistered(requiredType))
				return unityContainer.Resolve(requiredType);

            // Look in parent container.
            //if (parentContainer != null)
            //    return parentContainer.Resolve(requiredType);

            // Just try it any way...
            return unityContainer.Resolve(requiredType);
        }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            unityContainer.Dispose();
        }
    }
}
