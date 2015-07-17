using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neptuo.Logging
{
    /// <summary>
    /// Default implementation of <see cref="ILogFactory"/>.
    /// </summary>
    public class DefaultLogFactory : ILogFactory
    {
        private readonly List<ILogWriter> initialWriters;

        public string ScopeName { get; private set; }

        /// <summary>
        /// Creates new root log factory.
        /// No scope name will be used.
        /// </summary>
        public DefaultLogFactory()
            : this(String.Empty, Enumerable.Empty<ILogWriter>())
        { }

        /// <summary>
        /// Creates new log factory with <paramref name="scopeName"/>.
        /// </summary>
        /// <param name="scopeName">Scope name.</param>
        public DefaultLogFactory(string scopeName)
            : this(scopeName, Enumerable.Empty<ILogWriter>())
        { }

        /// <summary>
        /// Cretes new log factory with <paramref name="scopeName"/> and <paramref name="initialWriters"/>.
        /// </summary>
        /// <param name="scopeName">Scop name.</param>
        /// <param name="initialWriters">Enumeration of initial writers.</param>
        public DefaultLogFactory(string scopeName, IEnumerable<ILogWriter> initialWriters)
        {
            Ensure.NotNull(initialWriters, "parentWriters");
            ScopeName = scopeName;
            this.initialWriters = new List<ILogWriter>(initialWriters);
        }

        public ILog Scope(string scopeName)
        {
            Ensure.NotNullOrEmpty(scopeName, "scopeName");
            return new DefaultLog(ScopeName + "." + scopeName, initialWriters);
        }

        public ILogFactory AddWriter(ILogWriter writer)
        {
            Ensure.NotNull(writer, "writer");
            initialWriters.Add(writer);
            return this;
        }
    }
}
