using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Snapshots
{
    /// <summary>
    /// The exception used when the aggregate root doesn't support snapshots.
    /// </summary>
    [Serializable]
    public class SnapshotNotSupportedException : Exception
    {
        /// <summary>
        /// Creates new instance.
        /// </summary>
        public SnapshotNotSupportedException()
        { }
        
        /// <summary>
        /// Creates new instance for deserialization.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The streaming context.</param>
        protected SnapshotNotSupportedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
