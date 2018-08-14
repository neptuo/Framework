using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Generics
{
    /// <summary>
    /// A contract for deserializing objects that remembers which type was serialized.
    /// </summary>
    public interface IGenericDeserializer
    {
        /// <summary>
        /// Deserializes <paramref name="input"/> into <paramref name="context"/>.
        /// </summary>
        /// <param name="input">A serialized value to deserialize.</param>
        /// <param name="context">A context information of deserialization.</param>
        /// <returns><c>true</c> if deserialization was successful; <c>false</c> otherwise.</returns>
        bool TryDeserialize(Stream input, IGenericDeserializerContext context);

        /// <summary>
        /// Deserializes <paramref name="input"/> into <paramref name="context"/>.
        /// </summary>
        /// <param name="input">A serialized value to deserialize.</param>
        /// <param name="context">A context information of deserialization.</param>
        /// <returns>A continuation task containing information about deserialization success.</returns>
        Task<bool> TryDeserializeAsync(Stream input, IGenericDeserializerContext context);
    }
}
