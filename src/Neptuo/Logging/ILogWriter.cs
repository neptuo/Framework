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
        /// Dot separated log scope.
        /// </summary>
        string ScopeName { get; }

        /// <summary>
        /// Returns <c>true</c> if <paramref name="level"/> is enabled in current log; <c>false</c> otherwise.
        /// </summary>
        /// <param name="level">Log message level.</param>
        /// <returns><c>true</c> if <paramref name="level"/> is enabled in current log; <c>false</c> otherwise.</returns>
        bool IsLevelEnabled(LogLevel level);

        /// <summary>
        /// Logs <paramref name="message"/> to the current log with <paramref name="level"/>.
        /// </summary>
        /// <param name="level">Log message level.</param>
        /// <param name="message">Log message.</param>
        void Log(LogLevel level, string message);
    }
}
