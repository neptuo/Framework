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
    /// <summary>
    /// Implementation of <see cref="IDependencyContainer"/> with Unity container.
    /// </summary>
    public class UnityDependencyContainer : DisposableBase, IDependencyContainer
    {
        private readonly IUnityContainer unityContainer;
        private readonly RegistrationMapper mapper;

        public string ScopeName
        {
            get { return mapper.ScopeName; }
        }

        /// <summary>
        /// Creates container with new instance of Unity container.
        /// </summary>
        public UnityDependencyContainer()
            : this(new UnityContainer())
        { }

        /// <summary>
        /// Creates instance from <paramref name="unityContainer"/>.
        /// </summary>
        /// <param name="unityContainer">Unity container.</param>
        public UnityDependencyContainer(IUnityContainer unityContainer)
            : this(DependencyLifetime.RootScopeName, new MappingCollection(), unityContainer)
        { }

        private UnityDependencyContainer(string scopeName, MappingCollection mappings, IUnityContainer unityContainer)
        {
            Guard.NotNull(unityContainer, "unityContainer");
            this.unityContainer = unityContainer;
            this.mapper = new RegistrationMapper(unityContainer, mappings, scopeName);
        }

        public IDependencyContainer Map(Type requiredType, DependencyLifetime lifetime, object target)
        {
            mapper.Map(new MappingModel(requiredType, lifetime, target));
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
