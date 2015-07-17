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
        /// <summary>
        /// Serializer output writer.
        /// </summary>
        public TextWriter Output { get; private set; }

        /// <summary>
        /// Creates new instance that writes to the <paramref name="output"/>.
        /// </summary>
        /// <param name="output"></param>
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
