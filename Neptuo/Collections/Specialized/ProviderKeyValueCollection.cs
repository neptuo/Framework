using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Collections.Specialized
{
    using Provider = OutFunc<string, object, bool>;
    using Listener = Action<string, object>;

    /// <summary>
    /// Extensible <see cref="IKeyValueCollection"/>.
    /// When reading value, which has not been set yet, calls registered provider.
    /// Providers are of the two groups (distinguished by registration):
    /// a) for specific key (registered with key).
    /// b) general (for all keys).
    /// When trying to get value which key is not present, 
    /// first gets called provider registered with specific key, 
    /// than those registered without key.
    /// This collection also supports set listeners after set value in collection,
    /// these listener can be attached to specific key or to any key.
    /// </summary>
    public class ProviderKeyValueCollection : KeyValueCollection
    {
        /// <summary>
        /// Backing storage for providers.
        /// </summary>
        private readonly Dictionary<string, List<Provider>> providerStorage = new Dictionary<string, List<Provider>>();

        /// <summary>
        /// Backing storage for listeners.
        /// </summary>
        private readonly Dictionary<string, List<Listener>> listenerStorage = new Dictionary<string, List<Listener>>();

        /// <summary>
        /// Adds <paramref name="provider"/> for any key.
        /// This provider will be called after providers registered for specific key.
        /// </summary>
        /// <param name="provider">Provider for getting values.</param>
        public void AddProvider(Provider provider)
        {
            Guard.NotNull(provider, "provider");
            AddProvider(String.Empty, provider);
        }

        /// <summary>
        /// Adds <paramref name="provider"/> for specific key.
        /// </summary>
        /// <param name="key">Key for which <paramref name="provider"/> will be executed.</param>
        /// <param name="provider">Provider for getting values.</param>
        public void AddProvider(string key, Provider provider)
        {
            Guard.NotNull(key, "key");
            Guard.NotNull(provider, "provider");

            List<Provider> values;
            if (!providerStorage.TryGetValue(key, out values))
                values = providerStorage[key] = new List<Provider>();

            values.Add(provider);
        }

        /// <summary>
        /// Adds listener after setting value (with any key).
        /// </summary>
        /// <param name="listener">Listener executed after setting value.</param>
        public void AddListener(Listener listener)
        {
            Guard.NotNull(listener, "listener");
            AddListener(String.Empty, listener);
        }

        /// <summary>
        /// Adds listener after setting value (with any key).
        /// </summary>
        /// <param name="key">Key for which <paramref name="listener"/> will be executed.</param>
        /// <param name="listener">Listener executed after setting value.</param>
        public void AddListener(string key, Listener listener)
        {
            Guard.NotNull(key, "key");
            Guard.NotNull(listener, "listener");

            List<Listener> values;
            if (!listenerStorage.TryGetValue(key, out values))
                values = listenerStorage[key] = new List<Listener>();

            values.Add(listener);
        }

        public override IKeyValueCollection Set(string key, object value)
        {
            IKeyValueCollection result = base.Set(key, value);

            foreach (Listener listener in Enumerable.Concat(GetListeners(key), GetListeners(String.Empty)))
                listener(key, value);

            return result;
        }

        protected override bool TryGetDefault<T>(string key, out T value)
        {
            Guard.NotNull(key, "key");
            foreach (Provider provider in Enumerable.Concat(GetProviders(key), GetProviders(String.Empty)))
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

        private IEnumerable<Provider> GetProviders(string key)
        {
            List<Provider> values;
            if (!providerStorage.TryGetValue(key, out values))
                return Enumerable.Empty<Provider>();

            return values;
        }

        private IEnumerable<Listener> GetListeners(string key)
        {
            List<Listener> values;
            if (!listenerStorage.TryGetValue(key, out values))
                return Enumerable.Empty<Listener>();

            return values;
        }
    }
}
