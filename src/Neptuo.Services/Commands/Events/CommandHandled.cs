using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Commands.Events
{
    /// <summary>
    /// Event fired when command is handled by command handler.
    /// </summary>
    public class CommandHandled
    {
        /// <summary>
        /// Executed command.
        /// </summary>
        public object Command { get; private set; }

        /// <summary>
        /// Initializes new instance with <paramref name="command"/>, but no command guid.
        /// </summary>
        /// <param name="command">Executed command.</param>
        public CommandHandled(object command)
        {
            Ensure.NotNull(command, "command");
            Command = command;
        }
    }
}
