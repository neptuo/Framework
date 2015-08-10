using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Deleters
{
    /// <summary>
    /// Implementation of <see cref="IDeleteContext"/> for missing delete handler.
    /// </summary>
    public class MissingHandlerContext : IDeleteContext
    {
        public IKey SourceKey { get; private set; }
        
        public bool CanDelete
        {
            get { return false; }
        }

        public IEnumerable<IDeleteReference> References
        {
            get { return Enumerable.Empty<IDeleteReference>(); }
        }

        public MissingHandlerContext(IKey sourceKey)
        {
            Ensure.NotNull(sourceKey, "sourceKey");
            SourceKey = sourceKey;
        }

        public void Delete()
        {
            throw Ensure.Exception.NotSupported();
        }
    }
}
