using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators.Internals
{
    internal class MappingCollection
    {
        private readonly MappingCollection parentCollection;
        private readonly Dictionary<string, List<Mapping>> storage = new Dictionary<string, List<Mapping>>();

        public MappingCollection()
        { }

        public MappingCollection(MappingCollection parentCollection)
        {
            Guard.NotNull(parentCollection, "parentCollection");
            this.parentCollection = parentCollection;
        }

        public MappingCollection AddMapping(Type requiredType, DependencyLifetime lifetime, object target)
        {
            return AddMapping(new Mapping(requiredType, lifetime, target));
        }

        public MappingCollection AddMapping(Mapping model)
        {
            string scopeName;
            if (model.Lifetime.IsNamed)
                scopeName = model.Lifetime.Name;
            else if (model.Lifetime.IsScoped)
                scopeName = String.Empty;
            else
                throw Guard.Exception.InvalidOperation("MappingCollection supports only scoped or named scope registrations.");

            List<Mapping> models;
            if (!storage.TryGetValue(scopeName, out models))
                storage[scopeName] = models = new List<Mapping>();

            models.Add(model);
            return this;
        }

        public bool TryGet(string scopeName, out IEnumerable<Mapping> models)
        {
            List<Mapping> result;
            if (storage.TryGetValue(scopeName, out result))
            {
                models = result;

                IEnumerable<Mapping> parentResult;
                if (parentCollection != null && parentCollection.TryGet(scopeName, out parentResult))
                    models = Enumerable.Concat(result, parentResult);

                return true;
            }

            models = null;
            return false;
        }
    }
}
