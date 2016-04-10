using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data
{
    /// <summary>
    /// Describes serialized event.
    /// </summary>
    public class EventModel
    {
        /// <summary>
        /// The key to the aggregate where the original event raised.
        /// </summary>
        public IKey AggregateKey { get; set; }

        /// <summary>
        /// The key of the event.
        /// </summary>
        public IKey EventKey { get; set; }

        /// <summary>
        /// Serialized event payload.
        /// </summary>
        public string Payload { get; set; }

        /// <summary>
        /// The date and time when this event has raised.
        /// </summary>
        public DateTime RaisedAt { get; set; }

        /// <summary>
        /// Creates new empty instance.
        /// </summary>
        public EventModel()
        {
            RaisedAt = DateTime.Now;
        }

        /// <summary>
        /// Creates new instance and fills values.
        /// </summary>
        /// <param name="aggregateKey">The key to the aggregate where the original event raised.</param>
        /// <param name="eventKey">The key of the event.</param>
        /// <param name="payload">Serialized event payload.</param>
        public EventModel(IKey aggregateKey, IKey eventKey, string payload)
            : this()
        {
            Ensure.NotNull(aggregateKey, "aggregateKey");
            Ensure.NotNull(eventKey, "eventKey");
            Ensure.NotNull(payload, "payload");
            AggregateKey = aggregateKey;
            EventKey = eventKey;
            Payload = payload;
        }
    }
}
