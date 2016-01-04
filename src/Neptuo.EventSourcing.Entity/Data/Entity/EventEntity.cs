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
        public string AggregateID { get; set; }
        public string AggregateType { get; set; }
        public string Payload { get; set; }
        public DateTime RaisedAt { get; set; }

        public EventModel ToModel()
        {
            return new EventModel(StringKey.Create(AggregateID, AggregateType), Payload)
            {
                RaisedAt = RaisedAt
            };
        }

        public static EventEntity FromModel(EventModel model)
        {
            Ensure.NotNull(model, "model");
            return new EventEntity()
            {
                AggregateID = model.AggregateKey.Identifier,
                AggregateType = model.AggregateKey.Type,
                Payload = model.Payload,
                RaisedAt = model.RaisedAt
            };
        }
    }
}
