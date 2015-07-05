using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators.Internals
{
    internal class DependencyRegistry
    {
        private Dictionary<string, DependencyDefinition> registries;

        public DependencyRegistry()
            : this(new Dictionary<string, DependencyDefinition>())
        { }

        public DependencyRegistry(Dictionary<string, DependencyDefinition> registries)
        {
            Ensure.NotNull(registries, "registries");
            this.registries = registries;
        }

        public DependencyDefinition GetByKey(string key)
        {
            Ensure.NotNullOrEmpty(key, "key");

            DependencyDefinition item;
            if (registries.TryGetValue(key, out item))
                return item;

            return null;
        }

        public void Add(string key, DependencyDefinition item)
        {
            Ensure.NotNullOrEmpty(key, "key");
            Ensure.NotNull(item, "item");
            registries[key] = item;
        }

        public Dictionary<string, DependencyDefinition> CopyRegistries()
        {
            Dictionary<string, DependencyDefinition> result = new Dictionary<string, DependencyDefinition>();
            foreach (KeyValuePair<string, DependencyDefinition> item in registries)
                result.Add(item.Key, item.Value);

            return result;
        }
    }
}
