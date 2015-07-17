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
        private readonly IEnumerable<ILogWriter> writers;
        private ILogFactory factory;

        public string ScopeName { get; private set; }

        public ILogFactory Factory
        {
            get
            {
                if (factory == null)
                    factory = new DefaultLogFactory(ScopeName, writers);

                return factory;
            }
        }

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
            this.writers = writers;
            ScopeName = scopeName;
        }

        public bool IsLevelEnabled(LogLevel level)
        {
            return writers.Any(w => w.IsLevelEnabled(level));
        }

        public void Log(LogLevel level, string message)
        {
            foreach (ILogWriter writer in writers)
                writer.Log(level, message);
        }
    }
}
