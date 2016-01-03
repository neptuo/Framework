using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Entity
{
    /// <summary>
    /// Db context contract for storing event stream.
    /// </summary>
    public interface IEventContext
    {
        /// <summary>
        /// Stream of events.
        /// </summary>
        IDbSet<EventEntity> Events { get; }

        /// <summary>
        /// Saves change to the storage.
        /// </summary>
        void SaveChanges();
    }
}
