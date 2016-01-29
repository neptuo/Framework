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
        /// The type of the payload.
        /// </summary>
        public Type PayloadType { get; set; }

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
        /// <param name="payloadType">The of the payload.</param>
        /// <param name="payload">Serialized event payload.</param>
        public EventModel(IKey aggregateKey, Type payloadType, string payload)
            : this()
        {
            Ensure.NotNull(aggregateKey, "aggregateKey");
            Ensure.NotNull(payloadType, "payloadType");
            Ensure.NotNull(payload, "payload");
            AggregateKey = aggregateKey;
            PayloadType = payloadType;
            Payload = payload;
        }
    }
}
