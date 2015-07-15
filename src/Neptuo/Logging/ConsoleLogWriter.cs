using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Logging
{
    /// <summary>
    /// <see cref="Console"/> implementation of <see cref="ILogWriter"/>.
    /// </summary>
    public class ConsoleLogWriter : ILogWriter
    {
        public string ScopeName { get; private set; }
        public TextWriter Output { get; private set; }

        public ConsoleLogWriter(string scopeName)
        {
            ScopeName = scopeName;
            Output = Console.Out;
        }

        public bool IsLevelEnabled(LogLevel level)
        {
            return true;
        }

        public void Log(LogLevel level, string message)
        {
            Output.WriteLine(
                "{0}\t{1}: {2}", 
                DateTime.Now, 
                level.ToString().ToUpperInvariant(), 
                message
            );
        }
    }
}
