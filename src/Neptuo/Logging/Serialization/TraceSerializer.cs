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
        public bool IsEnabled(string scopeName, LogLevel level)
        {
            return true;
        }

        public void Append(string scopeName, LogLevel level, object model)
        {
            string message = String.Format("{0}\t{1}:{2}{3}{4}",
                DateTime.Now,
                scopeName,
                level.ToString().ToUpperInvariant(),
                Environment.NewLine,
                model
            );

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