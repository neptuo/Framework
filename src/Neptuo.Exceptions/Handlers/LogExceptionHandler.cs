using Neptuo;
using Neptuo.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Exceptions.Handlers
{
    /// <summary>
    /// An implementation of <see cref="IExceptionHandler"/> and <see cref="IExceptionHandler{T}"/> that logs all exceptions to <see cref="ILog"/>.
    /// All exceptions are logged as <see cref="Logging.LogLevel.Fatal"/> and this behavior can be overriden by <see cref="GetLevel(Exception)"/>.
    /// </summary>
    public class LogExceptionHandler : IExceptionHandler, IExceptionHandler<Exception>
    {
        private readonly ILog log;

        /// <summary>
        /// Creates new instance that logs exceptions to the <paramref name="log"/>.
        /// </summary>
        /// <param name="log">A log to write exceptions to.</param>
        public LogExceptionHandler(ILog log)
        {
            Ensure.NotNull(log, "log");
            this.log = log;
        }

        public void Handle(Exception exception)
        {
            log.Log(GetLevel(exception), exception);
        }

        /// <summary>
        /// Gets a <see cref="Logging.LogLevel"/> for <paramref name="exception"/>.
        /// </summary>
        /// <param name="exception">An exception to get log level for.</param>
        /// <returns>A log level for <paramref name="exception"/>.</returns>
        protected virtual LogLevel GetLevel(Exception exception)
        {
            return LogLevel.Fatal;
        }
    }
}
