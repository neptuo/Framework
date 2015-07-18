using Neptuo.Logging.Serialization.Filters;
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
    public class TraceSerializer : TextLogSerializerBase
    {
        /// <summary>
        /// Creates new instance that writes to the <see cref="Trace"/>.
        /// </summary>
        public TraceSerializer()
            : this(new DefaultLogFormatter(), new AllowedLogFilter())
        { }

        /// <summary>
        /// Creates new instance that writes to the <see cref="Trace"/>.
        /// </summary>
        /// <param name="formatter">Log entry formatter.</param>
        /// <param name="filter">Log entry filter.</param>
        public TraceSerializer(ILogFormatter formatter, ILogFilter filter)
            : base(formatter, filter)
        { }

        protected override void Append(string scopeName, LogLevel level, string message)
        {
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