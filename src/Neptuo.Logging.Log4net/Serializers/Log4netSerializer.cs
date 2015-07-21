using log4net;
using Neptuo.Logging.Serialization;
using Neptuo.Logging.Serialization.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ILogger = log4net.ILog;

namespace Neptuo.Logging.Serializers
{
    /// <summary>
    /// Basic integration of <see cref="log4net.ILog"/> to <see cref="ILogSerializer"/>.
    /// For formatting message uses <see cref="ILogFormatter"/> or tries to convert model to string using <see cref="Converts.Try"/>.
    /// </summary>
    public class Log4netSerializer : ILogSerializer
    {
        private readonly ILogFormatter formatter;

        /// <summary>
        /// Creates new instance that tries to serialize message model using <see cref="Converts.Try"/>.
        /// </summary>
        public Log4netSerializer()
        { }

        /// <summary>
        /// Creates new instance that serializes messages using <paramref name="formatter"/>.
        /// </summary>
        /// <param name="formatter">Log message formatter.</param>
        public Log4netSerializer(ILogFormatter formatter)
        {
            Ensure.NotNull(formatter, "formatter");
            this.formatter = formatter;
        }

        private ILogger GetLog(string scopeName)
        {
            return LogManager.GetLogger(scopeName);
        }

        public void Append(string scopeName, LogLevel level, object model)
        {
            ILogger log = GetLog(scopeName);

            // If requested level is not enabled, do nothing.
            if (!IsEnabled(log, level))
                return;

            object message;
            Exception exception = null;

            // If we have formatter, use formatter.
            if (formatter != null)
            {
                message = formatter.Format(scopeName, level, model);
            }
            else if (!Converts.Try(model.GetType(), typeof(string), model, out message))
            {
                // Try convert to exception model.
                ExceptionModel exceptionModel = model as ExceptionModel;
                if (exceptionModel != null)
                {
                    message = exceptionModel.Message;
                    exception = exceptionModel.Exception;
                }
                else
                {
                    // Use model as message.
                    message = model;
                }
            }

            // Log to defined level.
            switch (level)
            {
                case LogLevel.Debug:
                    log.Debug(model, exception);
                    break;
                case LogLevel.Info:
                    log.Info(model);
                    break;
                case LogLevel.Warning:
                    log.Warn(model, exception);
                    break;
                case LogLevel.Error:
                    log.Error(model, exception);
                    break;
                case LogLevel.Fatal:
                    log.Fatal(model, exception);
                    break;
            }
        }

        public bool IsEnabled(string scopeName, LogLevel level)
        {
            ILogger log = GetLog(scopeName);
            return IsEnabled(log, level);
        }

        private bool IsEnabled(ILogger log, LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    return log.IsDebugEnabled;
                case LogLevel.Info:
                    return log.IsInfoEnabled;
                case LogLevel.Warning:
                    return log.IsWarnEnabled;
                case LogLevel.Error:
                    return log.IsErrorEnabled;
                case LogLevel.Fatal:
                    return log.IsFatalEnabled;
                default:
                    return false;
            }
        }
    }
}
