using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators.Internals
{
    internal class UnityDependencyDefinitionCollection : IDependencyDefinitionCollection
    {
        private readonly UnityDependencyDefinitionCollection parentCollection;
        private readonly ConcurrentDictionary<string, List<UnityDependencyDefinition>> storage = new ConcurrentDictionary<string, List<UnityDependencyDefinition>>();

        public UnityDependencyDefinitionCollection()
        { }

        public UnityDependencyDefinitionCollection(UnityDependencyDefinitionCollection parentCollection)
        { 
            Ensure.NotNull(parentCollection, "parentCollection");
            this.parentCollection = parentCollection;
        }

        internal UnityDependencyDefinitionCollection AddMapping(UnityDependencyDefinition model)
        {
            string scopeName;
            if (model.Lifetime.IsNamed)
                scopeName = model.Lifetime.Name;
            else if (model.Lifetime.IsScoped)
                scopeName = String.Empty;
            else
                throw Ensure.Exception.InvalidOperation("MappingCollection supports only scoped or name-scoped registrations.");

            List<UnityDependencyDefinition> models;
            if (!storage.TryGetValue(scopeName, out models))
                storage[scopeName] = models = new List<UnityDependencyDefinition>();

            models.Add(model);
            return this;
        }

        public bool TryGet(string scopeName, out IEnumerable<UnityDependencyDefinition> models)
        {
            List<UnityDependencyDefinition> result;
            if (storage.TryGetValue(scopeName, out result))
            {
                models = result;

                IEnumerable<UnityDependencyDefinition> parentResult;
                if (parentCollection != null && parentCollection.TryGet(scopeName, out parentResult))
                    models = Enumerable.Concat(result, parentResult);

                return true;
            }

            models = null;
            return false;
        }

        #region IDependencyDefinitionCollection

        public IDependencyDefinitionCollection Add(Type requiredType, DependencyLifetime lifetime, object target)
        {
            return AddMapping(new UnityDependencyDefinition(requiredType, lifetime, target));
        }

        public bool TryGet(Type serviceType, out IDependencyDefinition definition)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
