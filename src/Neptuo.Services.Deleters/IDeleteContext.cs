using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Deleters
{
    /// <summary>
    /// Describes context of delete request.
    /// Takes referenced objects and whether object can be deleted.
    /// </summary>
    public interface IDeleteContext
    {
        /// <summary>
        /// Requested key to delete.
        /// </summary>
        IKey SourceKey { get; }

        /// <summary>
        /// If object identified by <see cref="IDeleteContext.SourceKey"/> can be deleted.
        /// </summary>
        bool CanDelete { get; }

        /// <summary>
        /// Collection of referenced objects.
        /// </summary>
        IEnumerable<IDeleteReference> References { get; }

        /// <summary>
        /// Executes delete (if can be deleted).
        /// </summary>
        /// <exception cref="InvalidOperationException">In case when <see cref="IDeleteContext.CanDelete"/> is <c>false</c>.</exception>
        void Delete();
    }
}
