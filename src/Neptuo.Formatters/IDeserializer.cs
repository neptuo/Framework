using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters
{
    /// <summary>
    /// The contract for deserializing objects.
    /// </summary>
    public interface IDeserializer
    {
        bool TryDeserialize(Stream input, IDeserializerContext context);

        /// <summary>
        /// Deserializes <paramref name="input"/> into <paramref name="context"/>.
        /// </summary>
        /// <param name="input">The serialized value to deserialize.</param>
        /// <param name="context">The context information of deserialization.</param>
        /// <returns>The continuation task containing information about serialization success.</returns>
        Task<bool> TryDeserializeAsync(Stream input, IDeserializerContext context);
    }
}
