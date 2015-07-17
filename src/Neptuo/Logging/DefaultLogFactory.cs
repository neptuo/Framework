using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neptuo.Logging
{
    public class DefaultLogFactory : ILogFactory
    {
        public string ScopeName { get; private set; }

        private readonly List<ILogWriter> childWriters;

        public DefaultLogFactory()
            : this(String.Empty, Enumerable.Empty<ILogWriter>())
        { }

        public DefaultLogFactory(string scopeName)
            : this(scopeName, Enumerable.Empty<ILogWriter>())
        { }

        public DefaultLogFactory(string scopeName, IEnumerable<ILogWriter> parentWriters)
        {
            Ensure.NotNull(parentWriters, "parentWriters");
            ScopeName = scopeName;
            this.childWriters = new List<ILogWriter>(parentWriters);
        }

        public ILog Scope(string scopeName)
        {
            Ensure.NotNullOrEmpty(scopeName, "scopeName");
            return new DefaultLog(ScopeName + "." + scopeName, childWriters);
        }

        public ILogFactory AddWriter(ILogWriter writer)
        {
            Ensure.NotNull(writer, "writer");
            childWriters.Add(writer);
            return this;
        }
    }
}
