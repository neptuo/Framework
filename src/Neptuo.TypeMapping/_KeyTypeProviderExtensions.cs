using Neptuo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Keys
{
    /// <summary>
    /// A 'Get' extensions for <see cref="IKeyTypeProvider"/>.
    /// </summary>
    public static class _KeyTypeProviderExtensions
    {
        /// <summary>
        /// Gets a <see cref="Type"/> for <paramref name="keyType"/>.
        /// </summary>
        /// <param name="provider">A mapping provider.</param>
        /// <param name="keyType">A <see cref="IKey.Type"/>.</param>
        /// <returns>An associated <see cref="Type"/> with <paramref name="keyType"/>.</returns>
        public static Type Get(this IKeyTypeProvider provider, string keyType)
        {
            Ensure.NotNull(provider, "provider");

            Type type;
            if (provider.TryGet(keyType, out type))
                return type;

            throw new MissingKeyTypeMappingException(keyType);
        }

        /// <summary>
        /// Gets a <see cref="IKey.Type"/> for <paramref name="type"/>.
        /// </summary>
        /// <param name="provider">A mapping provider.</param>
        /// <param name="type">A <see cref="Type"/>.</param>
        /// <returns>An associated <see cref="IKey.Type"/> with <paramref name="type"/>.</returns>
        public static string Get(this IKeyTypeProvider provider, Type type)
        {
            Ensure.NotNull(provider, "provider");

            string keyType;
            if (provider.TryGet(type, out keyType))
                return keyType;

            throw new MissingKeyTypeMappingException(type);
        }
    }
}
