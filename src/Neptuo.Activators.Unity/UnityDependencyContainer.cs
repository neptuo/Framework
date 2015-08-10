using Microsoft.Practices.Unity;
using Neptuo.Activators.Internals;
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
        private readonly DependencyDefinitionCollection definitions;

        /// <summary>
        /// Wrapped unity container.
        /// </summary>
        public IUnityContainer UnityContainer
        {
            get { return unityContainer; }
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
            : this(DependencyLifetime.RootScopeName, new DependencyDefinitionCollection(unityContainer, DependencyLifetime.RootScopeName), unityContainer)
        { }

        private UnityDependencyContainer(string scopeName, DependencyDefinitionCollection definitions, IUnityContainer unityContainer)
        {
            Ensure.NotNull(unityContainer, "unityContainer");
            this.unityContainer = unityContainer;
            this.definitions = definitions;

            unityContainer.RegisterInstance<IDependencyProvider>(this, new ExternallyControlledLifetimeManager());
            unityContainer.RegisterInstance<IDependencyContainer>(this, new ExternallyControlledLifetimeManager());
        }

        #region IDependencyContainer

        public IDependencyDefinitionCollection Definitions
        {
            get { return definitions; }
        }

        #endregion

        #region IDependencyProvider

        string IDependencyProvider.ScopeName
        {
            get { return definitions.Mapper.ScopeName; }
        }

        IDependencyDefinitionReadOnlyCollection IDependencyProvider.Definitions
        {
            get { return Definitions; }
        }

        IDependencyContainer IDependencyProvider.Scope(string scopeName)
        {
            IUnityContainer childContainer = unityContainer.CreateChildContainer();
            return new UnityDependencyContainer(
                scopeName,
                new DependencyDefinitionCollection(childContainer, scopeName, definitions),
                childContainer
            );
        }

        object IDependencyProvider.Resolve(Type requiredType)
        {
            Ensure.NotNull(requiredType, "requiredType");

            try
            {
                return unityContainer.Resolve(requiredType);
            }
            catch(ResolutionFailedException e)
            {
                throw Ensure.Exception.NotResolvable(requiredType, e);
            }
        }

        #endregion

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            unityContainer.Dispose();
        }
    }
}
