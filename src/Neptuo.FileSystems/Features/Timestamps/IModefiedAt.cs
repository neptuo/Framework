using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Features.Timestamps
{
    /// <summary>
    /// Provides timestamp at which item was modified.
    /// </summary>
    public interface IModefiedAt
    {
        /// <summary>
        /// Timestamp at which item was modified.
        /// </summary>
        DateTime CreatedAt { get; }
    }
}
