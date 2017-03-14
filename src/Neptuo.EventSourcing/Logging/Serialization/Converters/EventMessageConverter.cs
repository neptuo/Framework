using Neptuo.Converters;
using Neptuo.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Logging.Serialization.Converters
{
    /// <summary>
    /// A converter from <see cref="EventMessage"/> to <see cref="string"/>.
    /// </summary>
    public class EventMessageConverter : DefaultConverter<EventMessage, string>
    {
        public override bool TryConvert(EventMessage sourceValue, out string targetValue)
        {
            targetValue = $"{GetIdentifier(sourceValue.Event)}: {sourceValue.Message}";
            return true;
        }

        private static string GetIdentifier(IEvent payload)
        {
            if (payload == null)
                return "<null>";

            return $"<{payload.Key}>";
        }
    }
}
