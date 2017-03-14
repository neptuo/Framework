using Neptuo.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Logging
{
    /// <summary>
    /// A logging message for event.
    /// </summary>
    public class EventMessage
    {
        /// <summary>
        /// Gets an command; can be <c>null</c>.
        /// </summary>
        public IEvent Event { get; private set; }

        /// <summary>
        /// Gets a text message.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Create new instance.
        /// </summary>
        /// <param name="payload">An event.</param>
        /// <param name="message">A text message for string format.</param>
        /// <param name="parameters">Optional parameters for the <paramref name="message"/>.</param>
        public EventMessage(IEvent payload, string message, params object[] parameters)
        {
            Event = payload;
            Message = String.Format(message, parameters);
        }
    }
}
