using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Neptuo.Formatters
{
    /// <summary>
    /// System.Xml implementation of <see cref="IFormatter"/>.
    /// </summary>
    public class XmlFormatter : IFormatter, ISerializer, IDeserializer
    {
        /// <summary>
        /// Tries to catch XML related exceptions and process them as <c>false</c> result.
        /// </summary>
        /// <param name="handler">The action to decorate.</param>
        /// <returns><c>true</c> if <paramref name="handler"/> executed successfully; <c>false</c> otherwise.</returns>
        private static bool RunWithCatch(Action handler)
        {
            try
            {
                handler();
            }
            catch (InvalidOperationException)
            {
                return false;
            }
            catch (XmlException)
            {
                return false;
            }

            return true;
        }

        public Task<bool> TrySerializeAsync(object input, ISerializerContext context)
            => Task.Factory.StartNew(() => TrySerialize(input, context));

        public bool TrySerialize(object input, ISerializerContext context)
        {
            Ensure.NotNull(context, "context");
            return RunWithCatch(() =>
            {
                XmlSerializer serializer = new XmlSerializer(context.InputType);
                serializer.Serialize(context.Output, input);
            });
        }

        public Task<bool> TryDeserializeAsync(Stream input, IDeserializerContext context)
            => return Task.Factory.StartNew(() => TryDeserialize(input, context));

        public bool TryDeserialize(Stream input, IDeserializerContext context)
        {
            Ensure.NotNull(context, "context");
            return RunWithCatch(() =>
            {
                XmlSerializer serializer = new XmlSerializer(context.OutputType);
                object output = serializer.Deserialize(input);
                context.Output = output;
            });
        }
    }
}
