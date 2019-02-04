using Neptuo.Collections.Specialized;
using Neptuo.Events;
using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters
{
    /// <summary>
    /// Provides methods for setting <see cref="Event"/> key, aggregate key and version.
    /// </summary>
    public class EventExtender
    {
        /// <summary>
        /// Sets <see cref="Event.Key"/> to a <paramref name="eventKey"/>.
        /// </summary>
        /// <param name="payload">An event to change.</param>
        /// <param name="eventKey">A key to set as the event key.</param>
        public void SetKey(Event payload, IKey eventKey) => payload.Key = eventKey;

        /// <summary>
        /// Sets <see cref="Event.AggregateKey"/> to a <paramref name="aggregateKey"/>.
        /// </summary>
        /// <param name="payload">An event to change.</param>
        /// <param name="aggregateKey">A key to set as the event aggregate key.</param>
        public void SetAggregateKey(Event payload, IKey aggregateKey) => payload.AggregateKey = aggregateKey;

        /// <summary>
        /// Sets <see cref="Event.Version"/> to a <paramref name="version"/>.
        /// </summary>
        /// <param name="payload">An event to change.</param>
        /// <param name="version">A version to set as the event version.</param>
        public void SetVersion(Event payload, int version) => payload.Version = version;
    }
}
