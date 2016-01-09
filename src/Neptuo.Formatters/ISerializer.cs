using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters
{
    /// <summary>
    /// The contract for serializing objects.
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Serializes <paramref name="intput"/> to the <paramref name="context"/>.
        /// </summary>
        /// <param name="intput">The object to serialize.</param>
        /// <param name="context">The serialization context.</param>
        /// <returns>The continuation task containing information about serialization success.</returns>
        Task<bool> TrySerializeAsync(object intput, ISerializerContext context);
    }
}
