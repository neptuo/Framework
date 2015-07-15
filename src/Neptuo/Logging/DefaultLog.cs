using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Logging
{
    /// <summary>
    /// Default implementation of <see cref="ILog"/>.
    /// </summary>
    public class DefaultLog : ILog
    {
        private readonly IEnumerable<ILogWriter> currentWriters;
        private readonly List<ILogWriter> childWriters;

        public string ScopeName { get; private set; }

        /// <summary>
        /// Creates empty root log.
        /// </summary>
        public DefaultLog()
            : this("Root", Enumerable.Empty<ILogWriter>())
        { }

        /// <summary>
        /// Creates new instance with <paramref name="writers"/> in scope <paramref name="scopeName"/>.
        /// </summary>
        /// <param name="scopeName">Current log scope name.</param>
        /// <param name="writers">Writers for current log.</param>
        public DefaultLog(string scopeName, IEnumerable<ILogWriter> writers)
        {
            Ensure.NotNull(writers, "writers");
            this.currentWriters = writers;
            this.childWriters = new List<ILogWriter>();
            ScopeName = scopeName;
        }

        public ILog Scope(string scopeName)
        {
            Ensure.NotNullOrEmpty(scopeName, "scopeName");
            return new DefaultLog(ScopeName + "." + scopeName, Enumerable.Concat(currentWriters, childWriters));
        }

        public ILogFactory AddWriter(ILogWriter writer)
        {
            Ensure.NotNull(writer, "writer");
            childWriters.Add(writer);
            return this;
        }

        public bool IsLevelEnabled(LogLevel level)
        {
            return currentWriters.Any(w => w.IsLevelEnabled(level));
        }

        public void Log(LogLevel level, string message)
        {
            foreach (ILogWriter writer in currentWriters)
                writer.Log(level, message);
        }
    }
}
