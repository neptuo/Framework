using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data
{
    /// <summary>
    /// Describes serialized event.
    /// </summary>
    [DebuggerDisplay("{EventKey.Type} -> {AggregateKey.Type}")]
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
        /// The version of the aggregate root.
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// Creates new empty instance.
        /// </summary>
        public EventModel()
        { }

        /// <summary>
        /// Creates new instance and fills values.
        /// </summary>
        /// <param name="aggregateKey">The key to the aggregate where the original event raised.</param>
        /// <param name="eventKey">The key of the event.</param>
        /// <param name="payload">Serialized event payload.</param>
        /// <param name="version">The version of the aggregate root.</param>
        public EventModel(IKey aggregateKey, IKey eventKey, string payload, int version)
            : this()
        {
            Ensure.Condition.NotEmptyKey(aggregateKey);
            Ensure.Condition.NotEmptyKey(eventKey);
            Ensure.NotNull(payload, "payload");
            Ensure.Positive(version, "version");
            AggregateKey = aggregateKey;
            EventKey = eventKey;
            Payload = payload;
            Version = version;
        }
    }
}
