using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Logging
{
    /// <summary>
    /// Log contract.
    /// </summary>
    public interface ILog : ILogFactory
    {
        /// <summary>
        /// Dot separated log scope.
        /// </summary>
        string ScopeName { get; }

        /// <summary>
        /// Logs <paramref name="message"/> to the current log with <paramref name="level"/>.
        /// </summary>
        /// <param name="level">Log message level.</param>
        /// <param name="message">Log message.</param>
        void Log(LogLevel level, string message);
    }
}
