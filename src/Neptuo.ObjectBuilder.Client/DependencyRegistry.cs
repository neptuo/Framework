using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ObjectBuilder
{
    public class DependencyRegistry
    {
        private Dictionary<string, Dictionary<string, DependencyRegistryItem>> registries;

        public DependencyRegistry()
            : this(new Dictionary<string, Dictionary<string, DependencyRegistryItem>>())
        { }

        public DependencyRegistry(Dictionary<string, Dictionary<string, DependencyRegistryItem>> registries)
        {
            if (registries == null)
                throw new ArgumentNullException("registries");

            this.registries = registries;
        }

        public IEnumerable<DependencyRegistryItem> GetAllByKey(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (registries.ContainsKey(key))
                return registries[key].Values;

            return new List<DependencyRegistryItem>();
        }

        public DependencyRegistryItem GetByKey(string key, string subKey)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (registries.ContainsKey(key))
            {
                var subRegistry = registries[key];
                if (subKey == null)
                {
                    if (!subRegistry.ContainsKey(String.Empty))
                        throw new DependencyException("Missing default registry for key '" + key + "'.");

                    return subRegistry[String.Empty];
                }

                if (subRegistry.ContainsKey(subKey))
                    return subRegistry[subKey];

                return null;
            }
            return null;
        }

        public void Add(string key, string subKey, DependencyRegistryItem item)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (item == null)
                throw new ArgumentNullException("item");

            if (!registries.ContainsKey(key))
                registries[key] = new Dictionary<string, DependencyRegistryItem>();

            registries[key][subKey ?? String.Empty] = item;
        }

        public Dictionary<string, Dictionary<string, DependencyRegistryItem>> CopyRegistries()
        {
            Dictionary<string, Dictionary<string, DependencyRegistryItem>> result = new Dictionary<string, Dictionary<string, DependencyRegistryItem>>();
            foreach (KeyValuePair<string, Dictionary<string, DependencyRegistryItem>> item in registries)
            {
                Dictionary<string, DependencyRegistryItem> partialResult = new Dictionary<string, DependencyRegistryItem>();
                foreach (KeyValuePair<string, DependencyRegistryItem> subItem in item.Value)
                    partialResult.Add(subItem.Key, subItem.Value);

                result.Add(item.Key, partialResult);
            }
            return result;
        }
    }
}
