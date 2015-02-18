using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Pipelines.Commands.Events
{
    /// <summary>
    /// Event fired when command is handled by command handler.
    /// </summary>
    public class CommandHandled
    {
        /// <summary>
        /// Guid of executed command.
        /// Maybe <code>null</code> if command doesn't provide guid.
        /// </summary>
        public string CommandGuid { get; private set; }

        /// <summary>
        /// Executed command.
        /// </summary>
        public object Command { get; private set; }

        /// <summary>
        /// Initializes new instance with <paramref name="commandGuid"/> and <paramref name="command"/>.
        /// </summary>
        /// <param name="commandGuid">Guid of executed command.</param>
        /// <param name="command">Executed command.</param>
        public CommandHandled(string commandGuid, object command)
        {
            Guard.NotNullOrEmpty(commandGuid, "commandGuid");
            Guard.NotNull(command, "command");
            CommandGuid = commandGuid;
            Command = command;
        }

        /// <summary>
        /// Initializes new instance with <paramref name="command"/>, but no command guid.
        /// </summary>
        /// <param name="command">Executed command.</param>
        public CommandHandled(object command)
        {
            Guard.NotNull(command, "command");
            Command = command;
        }

        /// <summary>
        /// Initializes new instance with <paramref name="command"/> and takes <see cref="ICommand.Guid"/> as command guid.
        /// </summary>
        /// <param name="command">Executed command.</param>
        public CommandHandled(ICommand command)
        {
            Guard.NotNull(command, "command");
            CommandGuid = command.Guid;
            Command = command;
        }
    }
}
