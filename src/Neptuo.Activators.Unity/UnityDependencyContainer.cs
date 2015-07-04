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
            : this(DependencyLifetime.RootScopeName, new UnityDependencyDefinitionCollection(), unityContainer)
        { }

        private UnityDependencyContainer(string scopeName, UnityDependencyDefinitionCollection mappings, IUnityContainer unityContainer)
        {
            Ensure.NotNull(unityContainer, "unityContainer");
            this.unityContainer = unityContainer;
            this.mapper = new RegistrationMapper(unityContainer, mappings, scopeName);

            unityContainer.RegisterInstance<IDependencyProvider>(this, new ExternallyControlledLifetimeManager());
            unityContainer.RegisterInstance<IDependencyContainer>(this, new ExternallyControlledLifetimeManager());
        }

        #region IDependencyContainer

        public IDependencyDefinitionCollection Definitions
        {
            get { return mapper.Mappings; }
        }

        #endregion

        #region IDependencyProvider

        string IDependencyProvider.ScopeName
        {
            get { return mapper.ScopeName; }
        }

        IDependencyDefinitionReadOnlyCollection IDependencyProvider.Definitions
        {
            get { return Definitions; }
        }

        IDependencyContainer IDependencyProvider.Scope(string scopeName)
        {
            return new UnityDependencyContainer(
                scopeName,
                mapper.Mappings, 
                unityContainer.CreateChildContainer()
            );
        }

        object IDependencyProvider.Resolve(Type requiredType)
        {
            return unityContainer.Resolve(requiredType);
        }

        #endregion

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            unityContainer.Dispose();
        }
    }
}
