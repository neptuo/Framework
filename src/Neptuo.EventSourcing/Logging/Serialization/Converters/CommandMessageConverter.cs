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
    /// A converter from <see cref="CommandMessage"/> to <see cref="string"/>.
    /// </summary>
    public class CommandMessageConverter : DefaultConverter<CommandMessage, string>
    {
        public override bool TryConvert(CommandMessage sourceValue, out string targetValue)
        {
            targetValue = $"{GetIdentifier(sourceValue.Command)}: {sourceValue.Message}";
            return true;
        }

        private static string GetIdentifier(ICommand command)
        {
            if (command == null)
                return "<null>";

            return $"<{command.Key}>";
        }
    }
}
