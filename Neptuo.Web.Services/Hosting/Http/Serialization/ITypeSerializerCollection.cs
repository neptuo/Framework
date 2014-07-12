using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Http.Serialization
{
    /// <summary>
    /// Collection of type serializers.
    /// </summary>
    public interface ITypeSerializerCollection
    {
        /// <summary>
        /// Registers <paramref name="serializer"/> for objects of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Type of model to serialize.</typeparam>
        /// <param name="serializer">Serializer for object of type <typeparamref name="T"/>.</param>
        void AddSerializer<T>(ITypeSerializer<T> serializer);
    }
}
