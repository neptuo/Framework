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
    /// 
    /// </summary>
    internal class DependencyDefinitionMapper
    {
        private readonly IUnityContainer unityContainer;
        private readonly MappingCollection mappings;
        private readonly string scopeName;

        public string ScopeName
        {
            get { return scopeName; }
        }

        public MappingCollection Mappings
        {
            get { return mappings; }
        }

        public DependencyDefinitionMapper(IUnityContainer unityContainer, MappingCollection parentMappings, string scopeName)
        {
            Ensure.NotNull(unityContainer, "unityContainer");
            Ensure.NotNullOrEmpty(scopeName, "scopeName");
            this.unityContainer = unityContainer;
            this.scopeName = scopeName;

            if (parentMappings == null)
            {
                this.mappings = new MappingCollection();
            }
            else
            {
                this.mappings = new MappingCollection(parentMappings);
                CopyFromParentScope(parentMappings, scopeName);
            }
        }

        /// <summary>
        /// Copies definitions from ancestors for current scope.
        /// </summary>
        /// <param name="mappings">Ancestor definitions.</param>
        /// <param name="scopeName">Current scope name.</param>
        private void CopyFromParentScope(MappingCollection mappings, string scopeName)
        {
            IEnumerable<UnityDependencyDefinition> scopeMappings;
            if (mappings.TryGet(scopeName, out scopeMappings))
            {
                foreach (UnityDependencyDefinition model in scopeMappings)
                    Add(model);
            }
        }

        /// <summary>
        /// Adds definition, if <paramref name="definition"/>  is for current scope, registers it the unity container.
        /// Otherwise stores <paramref name="definition"/> in current mapping collection to any child scopes.
        /// </summary>
        /// <param name="definition">New dependency definition.</param>
        /// <returns>Self (for fluency).</returns>
        public DependencyDefinitionMapper Add(UnityDependencyDefinition definition)
        {
            // If lifetime is current scope, register to the unity container.
            if (!definition.Lifetime.IsNamed || definition.Lifetime.Name == scopeName)
            {
                LifetimeManager lifetimeManager = CreateLifetimeManager(definition.Lifetime);
                Register(unityContainer, definition.RequiredType, lifetimeManager, definition.Target);
            }

            // If lifetime is not transient, store for child scopes.
            if (!definition.Lifetime.IsTransient)
                mappings.AddMapping(definition);

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
            throw Ensure.Exception.NotSupported(lifetime.ToString());
        }

        //TODO: Implement using registered features...
        private void Register(IUnityContainer unityContainer, Type requiredType, LifetimeManager lifetimeManager, object target)
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

            // Nothing else is supported.
            throw Ensure.Exception.InvalidOperation("Not supported target type '{0}'.", target.GetType().FullName);
        }
    }
}
