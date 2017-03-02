using Neptuo.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Logging
{
    /// <summary>
    /// A logging message for command.
    /// </summary>
    public class CommandMessage
    {
        /// <summary>
        /// Gets an command.
        /// </summary>
        public ICommand Command { get; private set; }

        /// <summary>
        /// Gets a text message.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Create new instance.
        /// </summary>
        /// <param name="command">An command.</param>
        /// <param name="message">A text message for string format.</param>
        /// <param name="parameters">Optional parameters for the <paramref name="message"/>.</param>
        public CommandMessage(ICommand command, string message, params object[] parameters)
        {
            Command = command;
            Message = String.Format(message, parameters);
        }
    }
}
