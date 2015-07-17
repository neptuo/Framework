using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Logging
{
    /// <summary>
    /// Composite log contract.
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// Factory for child scopes.
        /// </summary>
        ILogFactory Factory { get; }

        /// <summary>
        /// Logs <paramref name="model"/> to the current log with <paramref name="level"/>.
        /// </summary>
        /// <param name="level">Log message level.</param>
        /// <param name="model">Log message.</param>
        void Log(LogLevel level, object model);
    }
}
