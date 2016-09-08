using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data
{
    /// <summary>
    /// The underlying store for events.
    /// </summary>
    public interface IEventStore
    {
        /// <summary>
        /// Returns enumeration of all events raised on <paramref name="aggregateKey"/>.
        /// </summary>
        /// <param name="aggregateKey">Key to the aggregate to load events of.</param>
        /// <returns>Enumeration of all events raised on <paramref name="aggregateKey"/>.</returns>
        IEnumerable<EventModel> Get(IKey aggregateKey);

        /// <summary>
        /// Saves <paramref name="events"/> to the underlying storage.
        /// </summary>
        /// <param name="events">The events to save.</param>
        void Save(IEnumerable<EventModel> events);
    }
}
