using Neptuo.Commands;
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
    /// A converter from <see cref="EnvelopeMessage"/> to <see cref="string"/>.
    /// </summary>
    public class EnvelopeMessageConverter : DefaultConverter<EnvelopeMessage, string>
    {
        public override bool TryConvert(EnvelopeMessage sourceValue, out string targetValue)
        {
            targetValue = $"{GetIdentifier(sourceValue.Envelope)}: {sourceValue.Message}";
            return true;
        }

        private static string GetIdentifier(Envelope envelope)
        {
            if (envelope == null)
                return "<null>";

            ICommand command = envelope.Body as ICommand;
            if (command != null)
                return $"<{command.Key}>";

            IEvent payload = envelope.Body as IEvent;
            if (payload != null)
                return $"<{payload.Key}>";

            return envelope.ToString();
        }
    }
}
