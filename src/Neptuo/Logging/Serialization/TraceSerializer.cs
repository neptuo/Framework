using Neptuo.Logging.Serialization.Formatters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Logging.Serialization
{
    /// <summary>
    /// Implementation of <see cref="ILogSerializer"/> that writes to the <see cref="Trace"/>.
    /// </summary>
    public class TraceSerializer : ILogSerializer
    {
        private readonly ILogFormatter formatter;

        /// <summary>
        /// Creates new instance that writes to the <see cref="Trace"/>.
        /// </summary>
        public TraceSerializer()
            : this(new DefaultLogFormatter())
        { }

        /// <summary>
        /// Creates new instance that writes to the <see cref="Trace"/> and formats entries using <paramref name="formatter"/>.
        /// </summary>
        /// <param name="formatter">Log entry formatter.</param>
        public TraceSerializer(ILogFormatter formatter)
        {
            Ensure.NotNull(formatter, "formatter");
            this.formatter = formatter;
        }

        public bool IsEnabled(string scopeName, LogLevel level)
        {
            return true;
        }

        public void Append(string scopeName, LogLevel level, object model)
        {
            string message = formatter.Format(scopeName, level, model);
            switch (level)
            {
                case LogLevel.Debug:
                case LogLevel.Info:
                    Trace.TraceInformation(message);
                    return;
                case LogLevel.Warning:
                    Trace.TraceWarning(message);
                    return;
                case LogLevel.Error:
                case LogLevel.Fatal:
                    Trace.TraceError(message);
                    return;
                default:
                    throw Ensure.Exception.NotSupported("Level '{0}' is not supported by the TraceSerializer.", level);
            }
        }
    }
}