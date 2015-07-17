using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Logging.Serialization
{
    /// <summary>
    /// Contract for writing to the log.
    /// </summary>
    public interface ILogSerializer
    {
        /// <summary>
        /// Returns <c>true</c> if combination <paramref name="scopeName" /> and <paramref name="level"/> is enabled; otherwise <c>false</c>.
        /// </summary>
        /// <param name="scopeName">Log scope.</param>
        /// <param name="level">Log message level.</param>
        /// <returns><c>true</c> if combination <paramref name="scopeName" /> and <paramref name="level"/> is enabled; otherwise <c>false</c>.</returns>
        bool IsEnabled(string scopeName, LogLevel level);

        /// <summary>
        /// Logs <paramref name="model"/> to the current log with <paramref name="level"/>.
        /// </summary>
        /// <param name="scopeName">Scope name to write into.</param>
        /// <param name="level">Log message level.</param>
        /// <param name="model">Log message.</param>
        void Append(string scopeName, LogLevel level, object model);
    }
}
