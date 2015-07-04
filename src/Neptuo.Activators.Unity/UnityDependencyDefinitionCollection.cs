using Microsoft.Practices.Unity;
using Neptuo.Activators.Internals;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators
{
    public class UnityDependencyDefinitionCollection : IDependencyDefinitionCollection
    {
        private readonly Dictionary<Type, UnityDependencyDefinition> storage = new Dictionary<Type, UnityDependencyDefinition>();
        private readonly IUnityContainer unityContainer;
        private readonly UnityDependencyDefinitionCollection parentCollection;

        public UnityDependencyDefinitionCollection(IUnityContainer unityContainer)
        {
            Ensure.NotNull(unityContainer, "unityContainer");
            this.unityContainer = unityContainer;
        }

        public UnityDependencyDefinitionCollection(IUnityContainer unityContainer, UnityDependencyDefinitionCollection parentCollection)
            : this(unityContainer)
        {
            Ensure.NotNull(parentCollection, "parentCollection");
            this.parentCollection = parentCollection;
        }

        public IDependencyDefinitionCollection Add(Type requiredType, DependencyLifetime lifetime, object target)
        {
            storage[requiredType] = new UnityDependencyDefinition(requiredType, lifetime, target);
            //TODO: Register to container.
            return this;
        }

        public bool TryGet(Type requiredType, out IDependencyDefinition definition)
        {
            Ensure.NotNull(requiredType, "requiredType");
            
            UnityDependencyDefinition result;
            if(storage.TryGetValue(requiredType, out result))
            {
                definition = result.Clone(IsResolvable(requiredType));
                return true;
            }

            if (parentCollection != null)
            {
                if (parentCollection.TryGet(requiredType, out definition))
                {
                    if (!definition.IsResolvable && IsResolvable(requiredType))
                        definition = new UnityDependencyDefinition(definition.RequiredType, definition.Lifetime, definition.Target, true);

                    return true;
                }
            }

            definition = null;
            return false;
        }

        private bool IsResolvable(Type requiredType)
        {
            return unityContainer.Registrations.Any(r => r.RegisteredType == requiredType);
        }
    }
}
