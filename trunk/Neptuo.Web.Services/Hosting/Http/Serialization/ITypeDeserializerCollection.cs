using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Http.Serialization
{
    /// <summary>
    /// Collection of type deserializers.
    /// </summary>
    public interface ITypeDeserializerCollection
    {
        /// <summary>
        /// Registers <paramref name="deserializer"/> for objects of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Type of model to deserialize.</typeparam>
        /// <param name="deserializer">Deserializer for object of type <typeparamref name="T"/>.</param>
        void AddDeserializer<T>(ITypeDeserializer<T> deserializer);
    }
}
