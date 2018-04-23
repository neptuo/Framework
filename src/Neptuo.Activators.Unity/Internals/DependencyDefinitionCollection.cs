using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Neptuo.Activators.Internals
{
    internal class DependencyDefinitionCollection : IDependencyDefinitionCollection
    {
        private readonly ConcurrentDictionary<Type, DependencyDefinition> definitionByRequiredType = new ConcurrentDictionary<Type, DependencyDefinition>();
        private readonly DependencyDefinitionCollection parentCollection;
        private readonly Mapper mapper;
        private readonly Func<Type, bool> isResolvable;

        internal Mapper Mapper
        {
            get { return mapper; }
        }

        public string ScopeName { get; private set; }

        private DependencyDefinitionCollection(IUnityContainer unityContainer, MapperCollection parentMappings, string scopeName)
        {
            Ensure.NotNull(unityContainer, "unityContainer");
            this.isResolvable = requiredType => unityContainer.Registrations.Any(r => r.RegisteredType == requiredType);
            this.mapper = new Mapper(unityContainer, parentMappings, scopeName);
            ScopeName = scopeName;
        }

        internal DependencyDefinitionCollection(IUnityContainer unityContainer, string scopeName)
            : this(unityContainer, null, scopeName)
        { }

        internal DependencyDefinitionCollection(IUnityContainer unityContainer, string scopeName, DependencyDefinitionCollection parentCollection)
            : this(unityContainer, parentCollection.Mapper.Mappings, scopeName)
        {
            Ensure.NotNull(parentCollection, "parentCollection");
            this.parentCollection = parentCollection;
        }

        public IDependencyDefinitionCollection Add(Type requiredType, DependencyLifetime lifetime, object target)
        {
            mapper.Add(definitionByRequiredType[requiredType] = new DependencyDefinition(requiredType, lifetime, target));
            return this;
        }

        public bool TryGet(Type requiredType, out IDependencyDefinition definition)
        {
            Ensure.NotNull(requiredType, "requiredType");
            
            DependencyDefinition result;
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
                        definition = new DependencyDefinition(definition.RequiredType, definition.Lifetime, definition.Target, true);

                    return true;
                }
            }

            definition = null;
            return false;
        }
    }
}
