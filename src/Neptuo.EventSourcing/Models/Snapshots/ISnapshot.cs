using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Snapshots
{
    /// <summary>
    /// The marker interface for aggregate root snapshot class.
    /// </summary>
    public interface ISnapshot
    {
        /// <summary>
        /// Returns key of the aggregate.
        /// </summary>
        IKey AggregateKey { get; }
    }
}
