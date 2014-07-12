using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Neptuo.Web.Services.Hosting.Http.Serialization
{
    /// <summary>
    /// Extended <see cref="XmlSerializerBase"/> with ability to delegate serialization and deserialization to type serializers/deserializers.
    /// </summary>
    public class XmlSerializer : XmlSerializerBase, ITypeSerializerCollection, ITypeDeserializerCollection
    {
        /// <summary>
        /// Collection of type serializers.
        /// </summary>
        private Dictionary<Type, object> typeSerializers = new Dictionary<Type, object>();

        /// <summary>
        /// Collection of type deserializers.
        /// </summary>
        private Dictionary<Type, object> typeDeserializers = new Dictionary<Type, object>();

        public void AddSerializer<T>(ITypeSerializer<T> serializer)
        {
            Guard.NotNull(serializer, "serializer");
            typeSerializers[typeof(T)] = serializer;
        }

        public void AddDeserializer<T>(ITypeDeserializer<T> deserializer)
        {
            Guard.NotNull(deserializer, "deserializer");
            typeDeserializers[typeof(T)] = deserializer;
        }
        
        public bool TrySerialize<T>(IHttpResponse response, T model)
        {
            object serializer;
            if (typeSerializers.TryGetValue(typeof(T), out serializer))
                return ((ITypeSerializer<T>)serializer).TrySerialize(response, model);

            return base.TrySerialize<T>(response, model);
        }

        public bool TryDeserialize<T>(IHttpRequest request, out T model)
        {
            object deserializer;
            if (typeDeserializers.TryGetValue(typeof(T), out deserializer))
                return ((ITypeDeserializer<T>)deserializer).TryDeserialize(request, out model);

            return base.TryDeserialize(request, out model);
        }
    }
}
