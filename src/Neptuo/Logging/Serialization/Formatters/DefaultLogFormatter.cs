using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Logging.Serialization.Formatters
{
    /// <summary>
    /// Default implementation of <see cref="ILogFormatter"/>.
    /// </summary>
    public class DefaultLogFormatter : ILogFormatter
    {
        public string Format(string scopeName, LogLevel level, object model)
        {
            return String.Format(
                "{0}\t{1}:{2}{3}{4}",
                DateTime.Now,
                scopeName,
                level.ToString().ToUpperInvariant(),
                Environment.NewLine,
                model
            );
        }
    }
}
