using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Logging.Serialization.Filters
{
    /// <summary>
    /// Implementation of <see cref="ILogFilter"/> that allows everything.
    /// </summary>
    public class AllowedLogFilter : ILogFilter
    {
        /// <summary>
        /// Gets a singleton.
        /// </summary>
        public static AllowedLogFilter Instance { get; } = new AllowedLogFilter();

        public bool IsEnabled(string scopeName, LogLevel level)
        {
            return true;
        }
    }
}
