using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators.Internals
{
    internal class DependencyRegistry
    {
        private Dictionary<string, DependencyRegistryItem> registries;

        public DependencyRegistry()
            : this(new Dictionary<string, DependencyRegistryItem>())
        { }

        public DependencyRegistry(Dictionary<string, DependencyRegistryItem> registries)
        {
            Guard.NotNull(registries, "registries");
            this.registries = registries;
        }

        public DependencyRegistryItem GetByKey(string key)
        {
            Guard.NotNullOrEmpty(key, "key");

            DependencyRegistryItem item;
            if (registries.TryGetValue(key, out item))
                return item;

            return null;
        }

        public void Add(string key, DependencyRegistryItem item)
        {
            Guard.NotNullOrEmpty(key, "key");
            Guard.NotNull(item, "item");
            registries[key] = item;
        }

        public Dictionary<string, DependencyRegistryItem> CopyRegistries()
        {
            Dictionary<string, DependencyRegistryItem> result = new Dictionary<string, DependencyRegistryItem>();
            foreach (KeyValuePair<string, DependencyRegistryItem> item in registries)
                result.Add(item.Key, item.Value);

            return result;
        }
    }
}
