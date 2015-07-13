using Microsoft.Practices.Unity;
using Neptuo.Activators.Internals.LifetimeManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators.Internals
{
    /// <summary>
    /// Provides mapping of ancestor registrations to the current unity container.
    /// </summary>
    internal class Mapper
    {
        private readonly IUnityContainer unityContainer;
        private readonly MapperCollection collection;
        private readonly string scopeName;

        public string ScopeName
        {
            get { return scopeName; }
        }

        public MapperCollection Mappings
        {
            get { return collection; }
        }

        /// <summary>
        /// Creates new instance for <paramref name="unityContainer"/> in scope <paramref name="scopeName"/>.
        /// If <paramref name="parentCollection"/> is <c>null</c>, thas mapper is the root one; otherwise this
        /// mapper inherits from <paramref name="parentCollection"/>.
        /// </summary>
        /// <param name="unityContainer">Unity container to insert definitions into.</param>
        /// <param name="parentCollection">Collection of definitions defined in parent scope.</param>
        /// <param name="scopeName">Scope name to associate with.</param>
        public Mapper(IUnityContainer unityContainer, MapperCollection parentCollection, string scopeName)
        {
            Ensure.NotNull(unityContainer, "unityContainer");
            Ensure.NotNullOrEmpty(scopeName, "scopeName");
            this.unityContainer = unityContainer;
            this.scopeName = scopeName;

            if (parentCollection == null)
            {
                this.collection = new MapperCollection();
            }
            else
            {
                this.collection = new MapperCollection(parentCollection);
                CopyFromParentScope(parentCollection, scopeName);
            }
        }

        /// <summary>
        /// Copies definitions from ancestors for current scope.
        /// </summary>
        /// <param name="mappings">Ancestor definitions.</param>
        /// <param name="scopeName">Current scope name.</param>
        private void CopyFromParentScope(MapperCollection mappings, string scopeName)
        {
            IEnumerable<DependencyDefinition> scopeMappings;
            if (mappings.TryGet(scopeName, out scopeMappings))
            {
                foreach (DependencyDefinition model in scopeMappings)
                    Add(model);
            }
        }

        /// <summary>
        /// Adds definition, if <paramref name="definition"/>  is for current scope, registers it the unity container.
        /// Otherwise stores <paramref name="definition"/> in current mapping collection to any child scopes.
        /// </summary>
        /// <param name="definition">New dependency definition.</param>
        /// <returns>Self (for fluency).</returns>
        public Mapper Add(DependencyDefinition definition)
        {
            // If lifetime is current scope, register to the unity container.
            if (!definition.Lifetime.IsNamed || definition.Lifetime.Name == scopeName)
            {
                LifetimeManager lifetimeManager = CreateLifetimeManager(definition.Lifetime, definition.Target is Type);
                Register(definition.RequiredType, lifetimeManager, definition.Target);
            }

            // If lifetime is not transient, store for child scopes.
            if (definition.Lifetime.IsNamed)
                collection.Add(definition);

            return this;
        }

        private LifetimeManager CreateLifetimeManager(DependencyLifetime lifetime, bool isMappedToType)
        {
            if (lifetime.IsTransient)
                return new TransientLifetimeManager();

            if (lifetime.IsNamed) 
            {
                if (isMappedToType)
                    return new ContainerControlledLifetimeManager();
                
                return new ExternallyControlledLifetimeManager();
            }

            if (lifetime.IsScoped)
                return new HierarchicalLifetimeManager();

            // Not supported lifetime.
            throw Ensure.Exception.NotSupported(lifetime.ToString());
        }

        //TODO: Implement using registered features...
        private void Register(Type requiredType, LifetimeManager lifetimeManager, object target)
        {
            // Target is type to map to.
            Type targetType = target as Type;
            if (targetType != null)
            {
                unityContainer.RegisterType(requiredType, targetType, lifetimeManager);
                return;
            }

            // Target is activator.
            IActivator<object> targetActivator = target as IActivator<object>;
            if (targetActivator != null)
            {
                unityContainer.RegisterType(requiredType, new ActivatorLifetimeManager(targetActivator, lifetimeManager));
                return;
            }

            // Target is instance of required type.
            targetType = target.GetType();
            if (requiredType.IsAssignableFrom(targetType))
            {
                unityContainer.RegisterInstance(requiredType, target);
                return;
            }

            // Target is type of activator for required type.
            Type requiredActivator = typeof(IActivator<>).MakeGenericType(requiredType);
            if (requiredActivator.IsAssignableFrom(targetType))
            {
                unityContainer.RegisterType(requiredActivator, new ActivatorTypeLifetimeManager(unityContainer, requiredActivator, lifetimeManager));
                return;
            }

            // Nothing else is supported.
            throw Ensure.Exception.InvalidOperation("Not supported target type '{0}'.", target.GetType().FullName);
        }
    }
}
