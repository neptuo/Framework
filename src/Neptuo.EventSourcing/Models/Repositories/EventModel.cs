﻿using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Repositories
{
    /// <summary>
    /// Describes serialized event.
    /// </summary>
    public class EventModel
    {
        /// <summary>
        /// Key to the aggregate where the original event raised.
        /// </summary>
        public GuidKey AggregateKey { get; set; }
        
        /// <summary>
        /// Serialized event payload.
        /// </summary>
        public string Payload { get; set; }

        /// <summary>
        /// Creates new empty instance.
        /// </summary>
        public EventModel()
        { }

        /// <summary>
        /// Creates new instance and fills values.
        /// </summary>
        /// <param name="aggregateKey">Key to the aggregate where the original event raised.</param>
        /// <param name="payload">Serialized event payload.</param>
        public EventModel(GuidKey aggregateKey, string payload)
        {
            Ensure.NotNull(aggregateKey, "aggregateKey");
            Ensure.NotNull(payload, "payload");
            AggregateKey = aggregateKey;
            Payload = payload;
        }
    }
}
