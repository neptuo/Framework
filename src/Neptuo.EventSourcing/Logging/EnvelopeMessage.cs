using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Logging
{
    /// <summary>
    /// A logging message for envelope.
    /// </summary>
    public class EnvelopeMessage
    {
        /// <summary>
        /// Gets an evenlope; can be <c>null</c>.
        /// </summary>
        public Envelope Envelope { get; private set; }

        /// <summary>
        /// Gets a text message.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Create new instance.
        /// </summary>
        /// <param name="envelope">An envelope.</param>
        /// <param name="message">A text message for string format.</param>
        /// <param name="parameters">Optional parameters for the <paramref name="message"/>.</param>
        public EnvelopeMessage(Envelope envelope, string message, params object[] parameters)
        {
            Envelope = envelope;
            Message = String.Format(message, parameters);
        }
    }
}
