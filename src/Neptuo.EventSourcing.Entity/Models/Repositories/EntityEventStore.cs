using Neptuo.Data.Entity;
using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Repositories
{
    public class EntityEventStore : IEventStore
    {
        private readonly IEventContext context;

        public EntityEventStore(IEventContext context)
        {
            Ensure.NotNull(context, "context");
            this.context = context;
        }

        public IEnumerable<EventModel> Get(GuidKey aggregateKey)
        {
            Ensure.Condition.NotEmptyKey(aggregateKey, "aggregateKey");

            IEnumerable<EventEntity> entities = context.Events
                .Where(e => e.AggregateType == aggregateKey.Type && e.AggregateID == aggregateKey.Guid)
                .OrderBy(e => e.RaisedAt);

            return entities.Select(e => e.ToModel());
        }

        public void Save(IEnumerable<EventModel> events)
        {
            Ensure.NotNull(events, "events");

            foreach (EventEntity entity in events.Select(EventEntity.FromModel))
                context.Events.Add(entity);

            context.SaveChanges();
        }
    }
}
