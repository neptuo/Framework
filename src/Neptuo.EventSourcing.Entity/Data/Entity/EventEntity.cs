using Neptuo.Models.Keys;
using Neptuo.Models.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Entity
{
    public class EventEntity
    {
        [Key]
        public Guid ID { get; set; }
        public string Type { get; set; }

        public Guid AggregateID { get; set; }
        public string AggregateType { get; set; }
        public string Payload { get; set; }
        public DateTime RaisedAt { get; set; }

        public EventModel ToModel()
        {
            //TODO: Fix PayloadType, maybe it is the typu to use in KEY.
            return new EventModel(GuidKey.Create(AggregateID, AggregateType), GuidKey.Create(ID, Type), Payload)
            {
                RaisedAt = RaisedAt
            };
        }

        public static EventEntity FromModel(EventModel model)
        {
            Ensure.NotNull(model, "model");

            GuidKey aggregateKey = model.AggregateKey as GuidKey;
            if (aggregateKey == null)
                throw Ensure.Exception.NotGuidKey(model.AggregateKey.GetType(), "aggregateKey");

            GuidKey eventKey = model.EventKey as GuidKey;
            if(eventKey == null)
                throw Ensure.Exception.NotGuidKey(model.EventKey.GetType(), "eventKey");
            
            return new EventEntity()
            {
                ID = eventKey.Guid,
                Type = eventKey.Type,

                AggregateID = aggregateKey.Guid,
                AggregateType = aggregateKey.Type,

                Payload = model.Payload,
                RaisedAt = model.RaisedAt
            };
        }
    }
}
