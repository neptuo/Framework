using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Keys
{
    /// <summary>
    /// A provider for mapping from <see cref="Type"/> to <see cref="IKey.Type"/> and vice-versa.
    /// </summary>
    public interface IKeyTypeProvider
    {
        /// <summary>
        /// Tries to get a clr type for <paramref name="keyType"/>.
        /// </summary>
        /// <param name="keyType">A <see cref="IKey.Type"/>.</param>
        /// <param name="type">An associated clr type.</param>
        /// <returns><c>true</c> if mapping was found and <paramref name="type"/> is set; <c>false</c> otherwise.</returns>
        bool TryGet(string keyType, out Type type);

        /// <summary>
        /// Tries to get a <see cref="IKey.Type"/> for <paramref name="type"/>.
        /// </summary>
        /// <param name="type">A clr type.</param>
        /// <param name="keyType">A <see cref="IKey.Type"/> for <paramref name="type"/>.</param>
        /// <returns><c>true</c> if mapping was found and <paramref name="keyType"/> is set; <c>false</c> otherwise.</returns>
        bool TryGet(Type type, out string keyType);
    }
}
