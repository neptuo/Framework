using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.TypeMapping
{
    /// <summary>
    /// A singleton access to <see cref="ITypeNameMapper"/>.
    /// </summary>
    public static class TypeNameMapper
    {
        private static readonly object providerLock = new object();
        private static ITypeNameMapper provider;

        /// <summary>
        /// Gets a singleton type name provider instance.
        /// </summary>
        public static ITypeNameMapper Default
        {
            get
            {
                if (provider == null)
                {
                    lock (providerLock)
                    {
                        if (provider == null)
                            provider = new AssemblyQualifiedNameMapper();
                    }
                }

                return provider;
            }
        }

        public static void SetProvider(ITypeNameMapper provider)
        {
            Ensure.NotNull(provider, "provider");

            lock (providerLock)
                TypeNameMapper.provider = provider;
        }
    }
}
