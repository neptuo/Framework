using Neptuo.Logging.Serialization;
using log4net;
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
    /// </summary>
    public class Log4netSerializer : ILogSerializer
    {
        private ILogger GetLog(string scopeName)
        {
            return LogManager.GetLogger(scopeName);
        }

        public void Append(string scopeName, LogLevel level, object model)
        {
            ILogger log = GetLog(scopeName);
            if(IsEnabled(log, level))
            {
                switch (level)
                {
                    case LogLevel.Debug:
                        log.Debug(model);
                        break;
                    case LogLevel.Info:
                        log.Info(model);
                        break;
                    case LogLevel.Warning:
                        log.Warn(model);
                        break;
                    case LogLevel.Error:
                        log.Error(model);
                        break;
                    case LogLevel.Fatal:
                        log.Fatal(model);
                        break;
                }
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
