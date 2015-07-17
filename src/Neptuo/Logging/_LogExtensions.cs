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
        /// Logs <paramref name="messageFormat"/> using <paramref name="parameters" /> to the <paramref name="log"/> at <see cref="LogLevel.Debug"/>.
        /// </summary>
        /// <param name="log">Log.</param>
        /// <param name="messageFormat">Log message format string.</param>
        /// <param name="parameters">Parameters for <paramref name="messageFormat"/>.</param>
        public static void Debug(this ILog log, string messageFormat, params object[] parameters)
        {
            Debug(log, String.Format(messageFormat, parameters));
        }


        /// <summary>
        /// Logs <paramref name="message"/> to the <paramref name="log"/> at <see cref="LogLevel.Info"/>.
        /// </summary>
        /// <param name="log">Log.</param>
        /// <param name="message">Log message.</param>
        public static void Info(this ILog log, string message)
        {
            Ensure.NotNull(log, "log");
            log.Log(LogLevel.Info, message);
        }

        /// <summary>
        /// Logs <paramref name="messageFormat"/> using <paramref name="parameters" /> to the <paramref name="log"/> at <see cref="LogLevel.Info"/>.
        /// </summary>
        /// <param name="log">Log.</param>
        /// <param name="messageFormat">Log message format string.</param>
        /// <param name="parameters">Parameters for <paramref name="messageFormat"/>.</param>
        public static void Info(this ILog log, string messageFormat, params object[] parameters)
        {
            Info(log, String.Format(messageFormat, parameters));
        }


        /// <summary>
        /// Logs <paramref name="message"/> to the <paramref name="log"/> at <see cref="LogLevel.Warning"/>.
        /// </summary>
        /// <param name="log">Log.</param>
        /// <param name="message">Log message.</param>
        public static void Warning(this ILog log, string message)
        {
            Ensure.NotNull(log, "log");
            log.Log(LogLevel.Warning, message);
        }

        /// <summary>
        /// Logs <paramref name="messageFormat"/> using <paramref name="parameters" /> to the <paramref name="log"/> at <see cref="LogLevel.Warning"/>.
        /// </summary>
        /// <param name="log">Log.</param>
        /// <param name="messageFormat">Log message format string.</param>
        /// <param name="parameters">Parameters for <paramref name="messageFormat"/>.</param>
        public static void Warning(this ILog log, string messageFormat, params object[] parameters)
        {
            Warning(log, String.Format(messageFormat, parameters));
        }


        /// <summary>
        /// Logs <paramref name="message"/> to the <paramref name="log"/> at <see cref="LogLevel.Error"/>.
        /// </summary>
        /// <param name="log">Log.</param>
        /// <param name="message">Log message.</param>
        public static void Error(this ILog log, string message)
        {
            Ensure.NotNull(log, "log");
            log.Log(LogLevel.Error, message);
        }

        /// <summary>
        /// Logs <paramref name="messageFormat"/> using <paramref name="parameters" /> to the <paramref name="log"/> at <see cref="LogLevel.Error"/>.
        /// </summary>
        /// <param name="log">Log.</param>
        /// <param name="messageFormat">Log message format string.</param>
        /// <param name="parameters">Parameters for <paramref name="messageFormat"/>.</param>
        public static void Error(this ILog log, string messageFormat, params object[] parameters)
        {
            Error(log, String.Format(messageFormat, parameters));
        }

        /// <summary>
        /// Logs <paramref name="message"/> to the <paramref name="log"/> at <see cref="LogLevel.Error"/>.
        /// </summary>
        /// <param name="log">Log.</param>
        /// <param name="exception">Log exception.</param>
        public static void Error(this ILog log, Exception exception)
        {
            Ensure.NotNull(log, "log");
            log.Log(LogLevel.Error, exception);
        }


        /// <summary>
        /// Logs <paramref name="message"/> to the <paramref name="log"/> at <see cref="LogLevel.Fatal"/>.
        /// </summary>
        /// <param name="log">Log.</param>
        /// <param name="message">Log message.</param>
        public static void Fatal(this ILog log, string message)
        {
            Ensure.NotNull(log, "log");
            log.Log(LogLevel.Fatal, message);
        }

        /// <summary>
        /// Logs <paramref name="messageFormat"/> using <paramref name="parameters" /> to the <paramref name="log"/> at <see cref="LogLevel.Fatal"/>.
        /// </summary>
        /// <param name="log">Log.</param>
        /// <param name="messageFormat">Log message format string.</param>
        /// <param name="parameters">Parameters for <paramref name="messageFormat"/>.</param>
        public static void Fatal(this ILog log, string messageFormat, params object[] parameters)
        {
            Fatal(log, String.Format(messageFormat, parameters));
        }

        /// <summary>
        /// Logs <paramref name="message"/> to the <paramref name="log"/> at <see cref="LogLevel.Fatal"/>.
        /// </summary>
        /// <param name="log">Log.</param>
        /// <param name="exception">Log exception.</param>
        public static void Fatal(this ILog log, Exception exception)
        {
            Ensure.NotNull(log, "log");
            log.Log(LogLevel.Fatal, exception);
        }
    }
}
