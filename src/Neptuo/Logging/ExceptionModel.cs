using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Logging
{
    /// <summary>
    /// Describes exception with message.
    /// </summary>
    public class ExceptionModel : IExceptionAware
    {
        /// <summary>
        /// Custom message associated with exception.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Raised exception.
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="message">Custom message associated with exception.</param>
        /// <param name="exception">Raised exception.</param>
        public ExceptionModel(string message, Exception exception)
        {
            Ensure.NotNullOrEmpty(message, "message");
            Ensure.NotNull(exception, "exception");
            Message = message;
            Exception = exception;
        }
    }
}
