using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters
{
    /// <summary>
    /// Newtonsoft.Json implementation of <see cref="IFormatter"/>.
    /// </summary>
    public class JsonFormatter : IFormatter, ISerializer, IDeserializer
    {
        private readonly JsonSerializerSettings settings;
        private readonly Formatting formatting;

        /// <summary>
        /// Creates a new instance with default settings and <see cref="Formatting.None"/> for serialization.
        /// </summary>
        public JsonFormatter()
            : this(new JsonSerializerSettings())
        { }

        /// <summary>
        /// Creates a new instance with <paramref name="settings" /> and <see cref="Formatting.None"/> for serialization.
        /// </summary>
        /// <param name="settings">A serializer and deserializer configuration.</param>
        public JsonFormatter(JsonSerializerSettings settings)
            : this(settings, Formatting.None)
        { }

        /// <summary>
        /// Create new instance with <paramref name="settings"/> and <paramref name="formatting"/>.
        /// </summary>
        /// <param name="settings">The serializer and deserializer configuration.</param>
        /// <param name="formatting">The indention formatting for serialization.</param>
        public JsonFormatter(JsonSerializerSettings settings, Formatting formatting)
        {
            Ensure.NotNull(settings, "settings");
            this.settings = settings;
            this.formatting = formatting;
        }

        public async Task<bool> TrySerializeAsync(object input, ISerializerContext context)
        {
            Ensure.NotNull(context, "context");

            //TODO: Catch exceptions.
            string result = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(input, formatting, settings));
            using (StreamWriter writer = new StreamWriter(context.Output, Encoding.UTF8, 1024, true))
                await writer.WriteAsync(result);

            return true;
        }

        public bool TrySerialize(object input, ISerializerContext context)
        {
            Ensure.NotNull(context, "context");

            //TODO: Catch exceptions.
            string result = JsonConvert.SerializeObject(input, formatting, settings);
            using (StreamWriter writer = new StreamWriter(context.Output, Encoding.UTF8, 1024, true))
                writer.Write(result);

            return true;
        }

        public async Task<bool> TryDeserializeAsync(Stream input, IDeserializerContext context)
        {
            Ensure.NotNull(context, "context");

            //TODO: Catch exceptions.
            using (StreamReader reader = new StreamReader(input))
            {
                string inputValue = await reader.ReadToEndAsync();
                context.Output = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject(inputValue, context.OutputType, settings));
            }

            return true;
        }

        public bool TryDeserialize(Stream input, IDeserializerContext context)
        {
            Ensure.NotNull(context, "context");

            //TODO: Catch exceptions.
            using (StreamReader reader = new StreamReader(input))
            {
                string inputValue = reader.ReadToEnd();
                context.Output = JsonConvert.DeserializeObject(inputValue, context.OutputType, settings);
            }

            return true;
        }
    }
}
