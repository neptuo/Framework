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
        public TextWriter Output { get; private set; }

        public ConsoleLogWriter()
        {
            Output = Console.Out;
        }

        public bool IsLevelEnabled(LogLevel level)
        {
            return true;
        }

        public void Log(string scopeName, LogLevel level, object model)
        {
            Output.WriteLine(
                "{0}\t{1}: {2}",
                DateTime.Now,
                level.ToString().ToUpperInvariant(),
                model
            );
        }
    }
}
