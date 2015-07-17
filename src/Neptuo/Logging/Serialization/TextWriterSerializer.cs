using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Logging.Serialization
{
    /// <summary>
    /// Base implementation of <see cref="ILogSerializer"/> that writes to the <see cref="TextWriter"/>.
    /// </summary>
    public class TextWriterSerializer : ILogSerializer
    {
        public TextWriter Output { get; private set; }

        public TextWriterSerializer(TextWriter output)
        {
            Ensure.NotNull(output, "output");
            Output = output;
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
                scopeName,
                level.ToString().ToUpperInvariant()
            );
            Output.WriteLine(model);
            Output.WriteLine();
        }
    }
}
