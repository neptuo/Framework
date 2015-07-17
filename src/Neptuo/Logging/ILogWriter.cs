using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Logging
{
    /// <summary>
    /// Contract for writing to the log.
    /// </summary>
    public interface ILogWriter
    {
        /// <summary>
        /// Returns <c>true</c> if <paramref name="level"/> is enabled in current log; <c>false</c> otherwise.
        /// </summary>
        /// <param name="level">Log message level.</param>
        /// <returns><c>true</c> if <paramref name="level"/> is enabled in current log; <c>false</c> otherwise.</returns>
        bool IsLevelEnabled(LogLevel level);

        /// <summary>
        /// Logs <paramref name="model"/> to the current log with <paramref name="level"/>.
        /// </summary>
        /// <param name="level">Log message level.</param>
        /// <param name="model">Log message.</param>
        void Log(string scopeName, LogLevel level, object model);
    }
}
