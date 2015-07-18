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
    /// <see cref="Console"/> implementation of <see cref="ILogSerializer"/>.
    /// </summary>
    public class ConsoleSerializer : TextWriterSerializer
    {
        /// <summary>
        /// Creates new instance that writes to the <see cref="Console.Out"/>.
        /// </summary>
        public ConsoleSerializer()
            : base(Console.Out)
        { }

        /// <summary>
        /// Creates new instance that writes to the <see cref="Console.Out"/> and formats entries using <paramref name="formatter"/>.
        /// </summary>
        /// <param name="formatter">Log entry formatter.</param>
        public ConsoleSerializer(ILogFormatter formatter)
            : base(Console.Out, formatter)
        { }
    }
}
