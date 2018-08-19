using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.TypeMapping
{
    /// <summary>
    /// An aggregated implementation of <see cref="ITypeNameMapper"/> which uses multiple providers to get value.
    /// </summary>
    public class AggregatedTypeNameMapper : ITypeNameMapper
    {
        private readonly object storageLock = new object();
        private readonly List<ITypeNameMapper> storage = new List<ITypeNameMapper>();

        /// <summary>
        /// Adds <paramref name="provider"/> to the collection of providers.
        /// </summary>
        /// <param name="provider">A provider to add.</param>
        /// <returns>Self (for fluency).</returns>
        public AggregatedTypeNameMapper Add(ITypeNameMapper provider)
        {
            Ensure.NotNull(provider, "provider");

            lock (storageLock)
                storage.Add(provider);

            return this;
        }

        public bool TryGet(string typeName, out Type type)
        {
            lock (storageLock)
            {
                foreach (ITypeNameMapper provider in storage)
                {
                    if (provider.TryGet(typeName, out type))
                        return true;
                }
            }

            type = null;
            return false;
        }

        public bool TryGet(Type type, out string typeName)
        {
            lock (storageLock)
            {
                foreach (ITypeNameMapper provider in storage)
                {
                    if (provider.TryGet(type, out typeName))
                        return true;
                }
            }

            typeName = null;
            return false;
        }
    }
}
