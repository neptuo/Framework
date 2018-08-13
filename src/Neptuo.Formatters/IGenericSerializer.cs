using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters
{
    /// <summary>
    /// A contract for serializer that remembers which type serializes.
    /// </summary>
    public interface IGenericSerializer
    {
        /// <summary>
        /// Serializes <paramref name="input"/> to the <paramref name="context"/>.
        /// </summary>
        /// <param name="input">An object to serialize.</param>
        /// <param name="context">An serialization context.</param>
        /// <returns><c>true</c> if serialization was successful; <c>false</c> otherwise.</returns>
        bool TrySerialize(object input, IGenericSerializerContext context);

        /// <summary>
        /// Serializes <paramref name="input"/> to the <paramref name="context"/>.
        /// </summary>
        /// <param name="input">An object to serialize.</param>
        /// <param name="context">A serialization context.</param>
        /// <returns>The continuation task containing information about serialization success.</returns>
        Task<bool> TrySerializeAsync(object input, IGenericSerializerContext context);
    }
}
