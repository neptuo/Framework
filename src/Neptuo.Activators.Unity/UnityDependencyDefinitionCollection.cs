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
        private readonly RegistrationMapper mapper;

        internal RegistrationMapper Mapper
        {
            get { return mapper; }
        }

        internal UnityDependencyDefinitionCollection(IUnityContainer unityContainer, MappingCollection mappings, string scopeName)
        {
            Ensure.NotNull(unityContainer, "unityContainer");
            this.unityContainer = unityContainer;
            this.mapper = new RegistrationMapper(unityContainer, mappings, scopeName);
        }

        internal UnityDependencyDefinitionCollection(IUnityContainer unityContainer, MappingCollection mappings, string scopeName, UnityDependencyDefinitionCollection parentCollection)
            : this(unityContainer, mappings, scopeName)
        {
            Ensure.NotNull(parentCollection, "parentCollection");
            this.parentCollection = parentCollection;
        }

        public IDependencyDefinitionCollection Add(Type requiredType, DependencyLifetime lifetime, object target)
        {
            mapper.Add(storage[requiredType] = new UnityDependencyDefinition(requiredType, lifetime, target));
            return this;
        }

        public UnityDependencyDefinitionCollection CreateChildCollection(IUnityContainer unityContainer, string scopeName)
        {
            return new UnityDependencyDefinitionCollection(unityContainer, new MappingCollection(mapper.Mappings), scopeName, this);
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
