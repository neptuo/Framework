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
        /// The stream of events.
        /// </summary>
        IDbSet<EventEntity> Events { get; }

        /// <summary>
        /// The stream of events that needs to be published on the dispatcher.
        /// </summary>
        IDbSet<UnPublishedEventEntity> UnPublishedEvents { get; }

        /// <summary>
        /// Saves change to the storage.
        /// </summary>
        void SaveChanges();
    }
}
