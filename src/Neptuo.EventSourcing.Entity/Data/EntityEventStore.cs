using Neptuo.Data;
using Neptuo.Data.Entity;
using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data
{
    public class EntityEventStore : IEventStore
    {
        private readonly IEventContext context;

        public EntityEventStore(IEventContext context)
        {
            Ensure.NotNull(context, "context");
            this.context = context;
        }

        public IEnumerable<EventModel> Get(IKey aggregateKey)
        {
            Ensure.Condition.NotEmptyKey(aggregateKey, "aggregateKey");

            StringKey key = aggregateKey as StringKey;
            if (key == null)
                throw Ensure.Exception.NotStringKey(aggregateKey.GetType(), "aggregateKey");

            IEnumerable<EventEntity> entities = context.Events
                .Where(e => e.AggregateType == key.Type && e.AggregateID == key.Identifier)
                .OrderBy(e => e.RaisedAt);

            return entities.Select(e => e.ToModel());
        }

        public void Save(IEnumerable<EventModel> events)
        {
            Ensure.NotNull(events, "events");

            foreach (EventEntity entity in events.Select(EventEntity.FromModel))
            {
                context.Events.Add(entity);
                context.UnPublishedEvents.Add(new UnPublishedEventEntity(entity));
            }

            context.SaveChanges();
        }
    }
}
