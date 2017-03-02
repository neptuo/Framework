using Neptuo.Commands;
using Neptuo.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Logging
{
    internal static class LogExtensions
    {
        public static void Debug(this ILog log, Envelope envelope, string message, params object[] parameters)
        {
            if (log.IsDebugEnabled())
                log.Debug(new EnvelopeMessage(envelope, message, parameters));
        }

        public static void Debug(this ILog log, ICommand command, string message, params object[] parameters)
        {
            if (log.IsDebugEnabled())
                log.Debug(new CommandMessage(command, message, parameters));
        }

        public static void Debug(this ILog log, IEvent payload, string message, params object[] parameters)
        {
            if (log.IsDebugEnabled())
                log.Debug(new EventMessage(payload, message, parameters));
        }

        public static void Debug(this ILog log, object model)
        {
            log.Log(LogLevel.Debug, model);
        }

        public static void Info(this ILog log, Envelope envelope, string message, params object[] parameters)
        {
            if (log.IsInfoEnabled())
                log.Info(new EnvelopeMessage(envelope, message, parameters));
        }

        public static void Info(this ILog log, ICommand command, string message, params object[] parameters)
        {
            if (log.IsInfoEnabled())
                log.Info(new CommandMessage(command, message, parameters));
        }

        public static void Info(this ILog log, IEvent payload, string message, params object[] parameters)
        {
            if (log.IsInfoEnabled())
                log.Info(new EventMessage(payload, message, parameters));
        }

        public static void Info(this ILog log, object model)
        {
            log.Log(LogLevel.Info, model);
        }

        public static void Warning(this ILog log, Envelope envelope, string message, params object[] parameters)
        {
            if (log.IsWarnEnabled())
                log.Warning(new EnvelopeMessage(envelope, message, parameters));
        }

        public static void Warning(this ILog log, ICommand command, string message, params object[] parameters)
        {
            if (log.IsWarnEnabled())
                log.Warning(new CommandMessage(command, message, parameters));
        }

        public static void Warning(this ILog log, IEvent payload, string message, params object[] parameters)
        {
            if (log.IsWarnEnabled())
                log.Warning(new EventMessage(payload, message, parameters));
        }

        public static void Warning(this ILog log, object model)
        {
            log.Log(LogLevel.Warning, model);
        }

        public static void Error(this ILog log, Envelope envelope, string message, params object[] parameters)
        {
            if (log.IsErrorEnabled())
                log.Error(new EnvelopeMessage(envelope, message, parameters));
        }

        public static void Error(this ILog log, ICommand command, string message, params object[] parameters)
        {
            if (log.IsErrorEnabled())
                log.Error(new CommandMessage(command, message, parameters));
        }

        public static void Error(this ILog log, IEvent payload, string message, params object[] parameters)
        {
            if (log.IsErrorEnabled())
                log.Error(new EventMessage(payload, message, parameters));
        }

        public static void Error(this ILog log, object model)
        {
            log.Log(LogLevel.Error, model);
        }

        public static void Fatal(this ILog log, Envelope envelope, string message, params object[] parameters)
        {
            if (log.IsFatalEnabled())
                log.Fatal(new EnvelopeMessage(envelope, message, parameters));
        }

        public static void Fatal(this ILog log, ICommand command, string message, params object[] parameters)
        {
            if (log.IsFatalEnabled())
                log.Fatal(new CommandMessage(command, message, parameters));
        }

        public static void Fatal(this ILog log, IEvent payload, string message, params object[] parameters)
        {
            if (log.IsFatalEnabled())
                log.Fatal(new EventMessage(payload, message, parameters));
        }

        public static void Fatal(this ILog log, object model)
        {
            log.Log(LogLevel.Fatal, model);
        }
    }
}
