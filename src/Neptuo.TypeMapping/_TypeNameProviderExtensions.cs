using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.TypeMapping
{
    /// <summary>
    /// A 'Get' extensions for <see cref="ITypeNameMapper"/>.
    /// </summary>
    public static class _TypeNameProviderExtensions
    {
        /// <summary>
        /// Gets a <see cref="Type"/> for <paramref name="typeName"/>.
        /// </summary>
        /// <param name="provider">A mapping provider.</param>
        /// <param name="typeName">A type name.</param>
        /// <returns>An associated <see cref="Type"/> with <paramref name="typeName"/>.</returns>
        public static Type Get(this ITypeNameMapper provider, string typeName)
        {
            Ensure.NotNull(provider, "provider");

            if (provider.TryGet(typeName, out Type type))
                return type;

            throw new MissingTypeNameMappingException(typeName);
        }

        /// <summary>
        /// Gets a <see cref="string"/> for <paramref name="type"/>.
        /// </summary>
        /// <param name="provider">A mapping provider.</param>
        /// <param name="type">A <see cref="Type"/>.</param>
        /// <returns>An associated <see cref="string"/> with <paramref name="type"/>.</returns>
        public static string Get(this ITypeNameMapper provider, Type type)
        {
            Ensure.NotNull(provider, "provider");

            if (provider.TryGet(type, out string typeName))
                return typeName;

            throw new MissingTypeNameMappingException(type);
        }
    }
}
