using Neptuo.Events;
using Neptuo.Formatters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Internals
{
    internal static class _FormatterExtensions
    {
        public static string SerializeEvent(this IFormatter formatter, IEvent payload)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                Task<bool> result = formatter.TrySerializeAsync(payload, new DefaultSerializerContext(payload.GetType(), stream));
                if (!result.IsCompleted)
                    result.Wait();

                if (result.Result)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    return Encoding.UTF8.GetString(stream.ToArray());
                }
            }

            throw Ensure.Exception.NotImplemented();
        }

        public static IEvent DeserializeEvent(this IFormatter formatter, Type eventType, string payload)
        {
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(payload)))
            {
                DefaultDeserializerContext context = new DefaultDeserializerContext(eventType);
                Task<bool> result = formatter.TryDeserializeAsync(stream, context);
                if (!result.IsCompleted)
                    result.Wait();

                if (result.Result)
                    return (IEvent)context.Output;
            }

            throw Ensure.Exception.NotImplemented();
        }
    }
}
