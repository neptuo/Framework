using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Generics
{
    /// <summary>
    /// A common extensions for <see cref="ISerializer"/> and <see cref="IGenericDeserializer"/>.
    /// </summary>
    public static class _FormatterExtensions
    {
        /// <summary>
        /// Serializes <paramref name="input"/> and returns string representation encoded in <see cref="Encoding.UTF8"/>.
        /// </summary>
        /// <param name="serializer">The serializer to use.</param>
        /// <param name="input">The input object to serialize.</param>
        /// <returns>The serialized <paramref name="input"/>.</returns>
        /// <exception cref="SerializationFailedException">When serialization was not sucessful.</exception>
        public static string Serialize(this ISerializer serializer, object input)
        {
            Ensure.NotNull(serializer, "serializer");
            Ensure.NotNull(input, "model");

            using (MemoryStream stream = new MemoryStream())
            {
                bool result = serializer.TrySerialize(input, new DefaultSerializerContext(stream));
                if (result)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    return Encoding.UTF8.GetString(stream.ToArray());
                }
            }

            throw new SerializationFailedException();
        }

        /// <summary>
        /// Serializes <paramref name="input"/> and returns string representation encoded in <see cref="Encoding.UTF8"/>.
        /// </summary>
        /// <param name="serializer">The serializer to use.</param>
        /// <param name="input">The input object to serialize.</param>
        /// <returns>The serialized <paramref name="input"/>.</returns>
        /// <exception cref="SerializationFailedException">When serialization was not sucessful.</exception>
        public static async Task<string> SerializeAsync(this ISerializer serializer, object input)
        {
            Ensure.NotNull(serializer, "serializer");
            Ensure.NotNull(input, "model");

            using (MemoryStream stream = new MemoryStream())
            {
                bool result = await serializer.TrySerializeAsync(input, new DefaultSerializerContext(stream));
                if (result)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    return Encoding.UTF8.GetString(stream.ToArray());
                }
            }

            throw new SerializationFailedException();
        }

        /// <summary>
        /// Deserializes <paramref name="input"/> to the object and returns it.
        /// </summary>
        /// <param name="deserializer">A deserializer to use.</param>
        /// <param name="input">A serialized input.</param>
        /// <returns>A deserialized object.</returns>
        /// <exception cref="DeserializationFailedException">When deserialization was not sucessful.</exception>
        public static object Deserialize(this IGenericDeserializer deserializer, string input)
        {
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(input)))
            {
                DefaultDeserializerContext context = new DefaultDeserializerContext();
                bool result = deserializer.TryDeserialize(stream, context);
                if (result)
                    return context.Output;
            }

            throw new DeserializationFailedException(input);
        }

        /// <summary>
        /// Deserializes <paramref name="input"/> to the object and returns it.
        /// </summary>
        /// <param name="deserializer">A deserializer to use.</param>
        /// <param name="input">A serialized input.</param>
        /// <returns>A continuation task containing a deserialized object.</returns>
        /// <exception cref="DeserializationFailedException">When deserialization was not sucessful.</exception>
        public static async Task<object> DeserializeAsync(this IGenericDeserializer deserializer, string input)
        {
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(input)))
            {
                DefaultDeserializerContext context = new DefaultDeserializerContext();
                bool result = await deserializer.TryDeserializeAsync(stream, context);
                if (result)
                    return context.Output;
            }

            throw new DeserializationFailedException(input);
        }
    }
}
