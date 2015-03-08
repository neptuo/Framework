using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators.Internals
{
    internal class MappingCollection
    {
        private readonly MappingCollection parentCollection;
        private readonly ConcurrentDictionary<string, List<MappingModel>> storage = new ConcurrentDictionary<string, List<MappingModel>>();

        public MappingCollection()
        { }

        public MappingCollection(MappingCollection parentCollection)
        {
            Guard.NotNull(parentCollection, "parentCollection");
            this.parentCollection = parentCollection;
        }

        public MappingCollection AddMapping(Type requiredType, DependencyLifetime lifetime, object target)
        {
            return AddMapping(new MappingModel(requiredType, lifetime, target));
        }

        public MappingCollection AddMapping(MappingModel model)
        {
            string scopeName;
            if (model.Lifetime.IsNamed)
                scopeName = model.Lifetime.Name;
            else if (model.Lifetime.IsScoped)
                scopeName = String.Empty;
            else
                throw Guard.Exception.InvalidOperation("MappingCollection supports only scoped or name-scoped registrations.");

            List<MappingModel> models;
            if (!storage.TryGetValue(scopeName, out models))
                storage[scopeName] = models = new List<MappingModel>();

            models.Add(model);
            return this;
        }

        public bool TryGet(string scopeName, out IEnumerable<MappingModel> models)
        {
            List<MappingModel> result;
            if (storage.TryGetValue(scopeName, out result))
            {
                models = result;

                IEnumerable<MappingModel> parentResult;
                if (parentCollection != null && parentCollection.TryGet(scopeName, out parentResult))
                    models = Enumerable.Concat(result, parentResult);

                return true;
            }

            models = null;
            return false;
        }
    }
}
