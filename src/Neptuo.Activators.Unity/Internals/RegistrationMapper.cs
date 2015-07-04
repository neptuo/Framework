﻿using Microsoft.Practices.Unity;
using Neptuo.Activators.Internals.LifetimeManagers;
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
        private readonly UnityDependencyDefinitionCollection mappings;
        private readonly string scopeName;

        public string ScopeName
        {
            get { return scopeName; }
        }

        public UnityDependencyDefinitionCollection Mappings
        {
            get { return mappings; }
        }

        public RegistrationMapper(IUnityContainer unityContainer, UnityDependencyDefinitionCollection parentMappings, string scopeName)
        {
            Ensure.NotNull(unityContainer, "unityContainer");
            Ensure.NotNullOrEmpty(scopeName, "scopeName");
            this.unityContainer = unityContainer;
            this.scopeName = scopeName;

            if (parentMappings == null)
            {
                this.mappings = new UnityDependencyDefinitionCollection();
            }
            else
            {
                this.mappings = new UnityDependencyDefinitionCollection(parentMappings);
                MapScope(parentMappings, scopeName);
            }
        }

        private void MapScope(UnityDependencyDefinitionCollection mappings, string scopeName)
        {
            IEnumerable<UnityDependencyDefinition> scopeMappings;
            if (mappings.TryGet(scopeName, out scopeMappings))
            {
                foreach (UnityDependencyDefinition model in scopeMappings)
                    Add(model);
            }
        }

        public RegistrationMapper Add(UnityDependencyDefinition model)
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
