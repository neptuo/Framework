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

        public string AggregateID { get; set; }
        public string AggregateType { get; set; }
        public string Payload { get; set; }
        public string PayloadType { get; set; }
        public DateTime RaisedAt { get; set; }

        public EventModel ToModel()
        {
            //TODO: Fix PayloadType, maybe it is the typu to use in KEY.
            return new EventModel(GuidKey.Create(ID, PayloadType), GuidKey.Create(Guid.Parse(AggregateID), AggregateType), Type.GetType(PayloadType), Payload)
            {
                RaisedAt = RaisedAt
            };
        }

        public static EventEntity FromModel(EventModel model)
        {
            Ensure.NotNull(model, "model");

            StringKey key = model.AggregateKey as StringKey;
            if (key == null)
                throw Ensure.Exception.NotStringKey(model.AggregateKey.GetType(), "aggregateKey");
            
            return new EventEntity()
            {
                AggregateID = key.Identifier,
                AggregateType = key.Type,
                Payload = model.Payload,
                PayloadType = model.PayloadType.AssemblyQualifiedName,
                RaisedAt = model.RaisedAt
            };
        }
    }
}
