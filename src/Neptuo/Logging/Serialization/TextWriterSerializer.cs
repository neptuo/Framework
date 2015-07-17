using Neptuo.Logging.Serialization.Formatters;
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
        /// Log entry formatter.
        /// </summary>
        public ILogFormatter Formatter { get; private set; }

        /// <summary>
        /// Creates new instance that writes to the <paramref name="output"/>.
        /// </summary>
        /// <param name="output">Serializer output writer.</param>
        public TextWriterSerializer(TextWriter output)
            : this(output, new DefaultLogFormatter())
        { }

        /// <summary>
        /// Creates new instance that writes to the <paramref name="output"/> and formats entries using <paramref name="formatter"/>.
        /// </summary>
        /// <param name="output">Serializer output writer.</param>
        /// <param name="formatter">Log entry formatter.</param>
        public TextWriterSerializer(TextWriter output, ILogFormatter formatter)
        {
            Ensure.NotNull(output, "output");
            Ensure.NotNull(formatter, "formatter");
            Output = output;
            Formatter = formatter;
        }

        public bool IsEnabled(string scopeName, LogLevel level)
        {
            return true;
        }

        public void Append(string scopeName, LogLevel level, object model)
        {
            Output.WriteLine(Formatter.Format(scopeName, level, model));
            Output.WriteLine();
        }
    }
}
