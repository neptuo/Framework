using Neptuo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Keys
{
    /// <summary>
    /// An aggregated implementation of <see cref="IKeyTypeProvider"/> which uses multiple providers to get value.
    /// </summary>
    public class AggregatedKeyTypeProvider : IKeyTypeProvider
    {
        private readonly object storageLock = new object();
        private readonly List<IKeyTypeProvider> storage = new List<IKeyTypeProvider>();

        /// <summary>
        /// Adds <paramref name="provider"/> to the collection of providers.
        /// </summary>
        /// <param name="provider">A provider to add.</param>
        /// <returns>Self (for fluency).</returns>
        public AggregatedKeyTypeProvider Add(IKeyTypeProvider provider)
        {
            Ensure.NotNull(provider, "provider");

            lock (storageLock)
                storage.Add(provider);

            return this;
        }

        public bool TryGet(string keyType, out Type type)
        {
            lock (storageLock)
            {
                foreach (IKeyTypeProvider provider in storage)
                {
                    if (provider.TryGet(keyType, out type))
                        return true;
                }
            }

            type = null;
            return false;
        }

        public bool TryGet(Type type, out string keyType)
        {
            lock (storageLock)
            {
                foreach (IKeyTypeProvider provider in storage)
                {
                    if (provider.TryGet(type, out keyType))
                        return true;
                }
            }

            keyType = null;
            return false;
        }
    }
}
