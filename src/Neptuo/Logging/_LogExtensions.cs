using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Logging
{
    /// <summary>
    /// Logging extensions for <see cref="ILogWriter"/>.
    /// </summary>
    public static class _LogExtensions
    {
        /// <summary>
        /// Logs <paramref name="message"/> to the <paramref name="log"/> at <see cref="LogLevel.Debug"/>.
        /// </summary>
        /// <param name="log">Log.</param>
        /// <param name="message">Log message.</param>
        public static void Debug(this ILog log, string message)
        {
            Ensure.NotNull(log, "log");
            log.Log(LogLevel.Debug, message);
        }

        /// <summary>
        /// Logs <paramref name="message"/> using <paramref name="parameters" /> to the <paramref name="log"/> at <see cref="LogLevel.Debug"/>.
        /// </summary>
        /// <param name="log">Log.</param>
        /// <param name="messageFormat">Log message format string.</param>
        /// <param name="parameters">Parameters for <paramref name="messageFormat"/>.</param>
        public static void Debug(this ILog log, string messageFormat, params object[] parameters)
        {
            Debug(log, String.Format(messageFormat, parameters));
        }
    }
}
