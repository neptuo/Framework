using Neptuo.Logging.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Logging
{
    public class StringSerializer : ILogSerializer
    {
        private readonly Func<string, LogLevel, bool> isLevelEnabled;

        public List<StringLogMessage> Messages { get; private set; }

        public StringSerializer()
            : this((scopeName, level) => true)
        { }

        public StringSerializer(Func<string, LogLevel, bool> isLevelEnabled)
        {
            Messages = new List<StringLogMessage>();
            this.isLevelEnabled = isLevelEnabled;
        }

        public bool IsEnabled(string scopeName, LogLevel level)
        {
            return isLevelEnabled(scopeName, level);
        }

        public void Append(string scopeName, LogLevel level, object model)
        {
            if (IsEnabled(scopeName, level))
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
