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
        private readonly ConcurrentDictionary<Type, UnityDependencyDefinition> definitionByRequiredType = new ConcurrentDictionary<Type, UnityDependencyDefinition>();
        private readonly UnityDependencyDefinitionCollection parentCollection;
        private readonly DependencyDefinitionMapper mapper;
        private readonly Func<Type, bool> isResolvable;

        internal DependencyDefinitionMapper Mapper
        {
            get { return mapper; }
        }

        private UnityDependencyDefinitionCollection(IUnityContainer unityContainer, MappingCollection parentMappings, string scopeName)
        {
            Ensure.NotNull(unityContainer, "unityContainer");
            this.isResolvable = requiredType => unityContainer.Registrations.Any(r => r.RegisteredType == requiredType);
            this.mapper = new DependencyDefinitionMapper(unityContainer, parentMappings, scopeName);
        }

        internal UnityDependencyDefinitionCollection(IUnityContainer unityContainer, string scopeName)
            : this(unityContainer, null, scopeName)
        { }

        internal UnityDependencyDefinitionCollection(IUnityContainer unityContainer, string scopeName, UnityDependencyDefinitionCollection parentCollection)
            : this(unityContainer, parentCollection.Mapper.Mappings, scopeName)
        {
            Ensure.NotNull(parentCollection, "parentCollection");
            this.parentCollection = parentCollection;
        }

        public IDependencyDefinitionCollection Add(Type requiredType, DependencyLifetime lifetime, object target)
        {
            mapper.Add(definitionByRequiredType[requiredType] = new UnityDependencyDefinition(requiredType, lifetime, target));
            return this;
        }

        public bool TryGet(Type requiredType, out IDependencyDefinition definition)
        {
            Ensure.NotNull(requiredType, "requiredType");
            
            UnityDependencyDefinition result;
            if(definitionByRequiredType.TryGetValue(requiredType, out result))
            {
                definition = result.Clone(isResolvable(requiredType));
                return true;
            }

            if (parentCollection != null)
            {
                if (parentCollection.TryGet(requiredType, out definition))
                {
                    if (!definition.IsResolvable && isResolvable(requiredType))
                        definition = new UnityDependencyDefinition(definition.RequiredType, definition.Lifetime, definition.Target, true);

                    return true;
                }
            }

            definition = null;
            return false;
        }
    }
}
