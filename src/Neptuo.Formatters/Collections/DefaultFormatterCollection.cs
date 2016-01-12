using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Collections
{
    /// <summary>
    /// Default implementation of <see cref="ISerializerCollection"/> and <see cref="IDeserializerCollection"/>.
    /// </summary>
    public class DefaultFormatterCollection<TKey> : ISerializerCollection<TKey>, IDeserializerCollection<TKey>
    {
        private readonly Dictionary<TKey, ISerializer> serializers = new Dictionary<TKey, ISerializer>();
        private readonly Dictionary<TKey, IDeserializer> deserializers = new Dictionary<TKey, IDeserializer>();

        public ISerializerCollection<TKey> Add(TKey key, ISerializer serializer)
        {
            Ensure.NotNull(key, "key");
            Ensure.NotNull(serializer, "serializer");
            serializers[key] = serializer;
            return this;
        }

        public IDeserializerCollection<TKey> Add(TKey key, IDeserializer deserializer)
        {
            Ensure.NotNull(key, "key");
            Ensure.NotNull(deserializer, "deserializer");
            deserializers[key] = deserializer;
            return this;
        }

        public bool TryGet(TKey key, out ISerializer serializer)
        {
            Ensure.NotNull(key, "key");
            return serializers.TryGetValue(key, out serializer);
        }

        public bool TryGet(TKey key, out IDeserializer deserializer)
        {
            Ensure.NotNull(key, "key");
            return deserializers.TryGetValue(key, out deserializer);
        }
    }
}
