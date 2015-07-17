using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Logging
{
    public class StringLogWriter : ILogWriter
    {
        private readonly Func<LogLevel, bool> isLevelEnabled;

        public List<StringLogMessage> Messages { get; private set; }

        public StringLogWriter(Func<LogLevel, bool> isLevelEnabled)
        {
            Messages = new List<StringLogMessage>();
            this.isLevelEnabled = isLevelEnabled;
        }

        public bool IsLevelEnabled(LogLevel level)
        {
            return isLevelEnabled(level);
        }

        public void Log(string scopeName, LogLevel level, object model)
        {
            if (IsLevelEnabled(level))
            {
                Messages.Add(new StringLogMessage
                {
                    Level = level,
                    ScopeName = scopeName,
                    Model = model
                });
            }
        }
    }

    public class StringLogMessage
    {
        public LogLevel Level { get; set; }
        public string ScopeName { get; set; }
        public object Model { get; set; }
    }
}
