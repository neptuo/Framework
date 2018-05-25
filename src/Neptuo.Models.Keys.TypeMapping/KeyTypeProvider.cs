using Neptuo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Keys
{
    /// <summary>
    /// A singleton access to <see cref="IKeyTypeProvider"/>.
    /// </summary>
    public static class KeyTypeProvider
    {
        private static readonly object providerLock = new object();
        private static IKeyTypeProvider provider;

        /// <summary>
        /// Gets a singleton key type provider instance.
        /// </summary>
        public static IKeyTypeProvider Default
        {
            get
            {
                if (provider == null)
                {
                    lock (providerLock)
                    {
                        if (provider == null)
                            provider = new AssemblyQualifiedKeyTypeProvider();
                    }
                }

                return provider;
            }
        }

        public static void SetProvider(IKeyTypeProvider provider)
        {
            Ensure.NotNull(provider, "provider");

            lock (providerLock)
                KeyTypeProvider.provider = provider;
        }
    }
}
