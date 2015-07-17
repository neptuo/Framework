using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Logging.Serialization
{
    /// <summary>
    /// <see cref="Console"/> implementation of <see cref="ILogSerializer"/>.
    /// </summary>
    public class ConsoleSerializer : ILogSerializer
    {
        public TextWriter Output { get; private set; }

        public ConsoleSerializer()
        {
            Output = Console.Out;
        }

        public bool IsEnabled(string scopeName, LogLevel level)
        {
            return true;
        }

        public void Append(string scopeName, LogLevel level, object model)
        {
            Output.WriteLine(
                "{0}\t{1}:{2}",
                DateTime.Now,
                level.ToString().ToUpperInvariant(),
                model
            );
            Output.WriteLine(model);
            Output.WriteLine();
        }
    }
}
