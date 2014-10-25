using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Collections.Specialized
{
    /// <summary>
    /// Extensible <see cref="IKeyValueCollection"/>.
    /// When reading value, which has not been set yet, calls registered provider.
    /// Providers are of the two groups (distinguished by registration):
    /// a) for specific key (registered with key).
    /// b) general (for all keys).
    /// When trying to get value which key is not present, 
    /// first gets called provider registered with specific key, 
    /// than those registered without key.
    /// </summary>
    public class ProviderKeyValueCollection : KeyValueCollection
    {
        /// <summary>
        /// Backing storage for providers.
        /// </summary>
        private readonly Dictionary<string, List<OutFunc<string, object, bool>>> storage = new Dictionary<string, List<OutFunc<string, object, bool>>>();

        /// <summary>
        /// Mapps <paramref name="provider"/>.
        /// This provider will be called after providers registered for specific key.
        /// </summary>
        /// <param name="provider">Provider for getting values.</param>
        public void AddProvider(OutFunc<string, object, bool> provider)
        {
            Guard.NotNull(provider, "provider");
            AddProvider(String.Empty, provider);
        }

        public void AddProvider(string key, OutFunc<string, object, bool> provider)
        {
            Guard.NotNull(key, "key");
            Guard.NotNull(provider, "provider");

            List<OutFunc<string, object, bool>> values;
            if (!storage.TryGetValue(key, out values))
                values = storage[key] = new List<OutFunc<string, object, bool>>();

            values.Add(provider);
        }

        protected override bool TryGetDefault<T>(string key, out T value)
        {
            Guard.NotNull(key, "key");
            foreach (OutFunc<string, object, bool> provider in Enumerable.Concat(GetProviders(key), GetProviders(String.Empty)))
            {
                object valueBase;
                if(provider(key, out valueBase))
                {
                    Set(key, valueBase);
                    return ConvertTo(valueBase, out value);
                }
            }

            return base.TryGetDefault(key, out value);
        }

        private IEnumerable<OutFunc<string, object, bool>> GetProviders(string key)
        {
            List<OutFunc<string, object, bool>> values;
            if (!storage.TryGetValue(key, out values))
                return Enumerable.Empty<OutFunc<string, object, bool>>();

            return values;
        }
    }
}
