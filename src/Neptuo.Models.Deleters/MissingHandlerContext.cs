using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Deleters
{
    /// <summary>
    /// An implementation of <see cref="IDeleteContext"/> for missing delete handler.
    /// </summary>
    public class MissingHandlerContext : IDeleteContext
    {
        public IKey SourceKey { get; }
        
        public bool CanDelete => false;

        public IEnumerable<IDeleteReference> References => Enumerable.Empty<IDeleteReference>();

        /// <summary>
        /// Creates a new instance for a <paramref name="sourceKey"/>.
        /// </summary>
        /// <param name="sourceKey">A key of the object.</param>
        public MissingHandlerContext(IKey sourceKey)
        {
            Ensure.NotNull(sourceKey, "sourceKey");
            SourceKey = sourceKey;
        }

        public void Delete() => throw Ensure.Exception.NotSupported();
    }
}
