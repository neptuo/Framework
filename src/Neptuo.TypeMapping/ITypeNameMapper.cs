using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.TypeMapping
{
    /// <summary>
    /// A provider for mapping from <see cref="Type"/> to <see cref="string"/> and vice-versa.
    /// </summary>
    public interface ITypeNameMapper
    {
        /// <summary>
        /// Tries to get a clr type for <paramref name="typeName"/>.
        /// </summary>
        /// <param name="typeName">A <see cref="string"/>.</param>
        /// <param name="type">An associated clr type.</param>
        /// <returns><c>true</c> if mapping was found and <paramref name="type"/> is set; <c>false</c> otherwise.</returns>
        bool TryGet(string typeName, out Type type);

        /// <summary>
        /// Tries to get a <see cref="string"/> for <paramref name="type"/>.
        /// </summary>
        /// <param name="type">A clr type.</param>
        /// <param name="typeName">A <see cref="string"/> for <paramref name="type"/>.</param>
        /// <returns><c>true</c> if mapping was found and <paramref name="typeName"/> is set; <c>false</c> otherwise.</returns>
        bool TryGet(Type type, out string typeName);
    }
}
