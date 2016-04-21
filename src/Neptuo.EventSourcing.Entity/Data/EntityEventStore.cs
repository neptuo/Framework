using Neptuo.Data;
using Neptuo.Data.Entity;
using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data
{
    public class EntityEventStore : IEventStore, IEventPublishingStore
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

            GuidKey key = aggregateKey as GuidKey;
            if (key == null)
                throw Ensure.Exception.NotGuidKey(aggregateKey.GetType(), "aggregateKey");

            IEnumerable<EventEntity> entities = context.Events
                .Where(e => e.AggregateType == key.Type && e.AggregateID == key.Guid)
                .OrderBy(e => e.ID);

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

            context.Save();
        }

        public async Task<IEnumerable<EventPublishingModel>> GetAsync()
        {
            return await context.UnPublishedEvents
                .Select(e => new EventPublishingModel(e.Event.ToModel(), e.PublishedToHandlers.Select(h => h.HandlerIdentifier)))
                .ToListAsync();
        }

        public Task PublishedAsync(IKey key, string handlerIdentifier)
        {
            GuidKey eventKey = key as GuidKey;
            if (eventKey == null)
                throw Ensure.Exception.NotGuidKey(eventKey.GetType(), "key");

            UnPublishedEventEntity entity = context.UnPublishedEvents.FirstOrDefault(e => e.Event.EventType == eventKey.Type && e.Event.EventID == eventKey.Guid);
            if (entity == null)
                return Task.FromResult(true);

            entity.PublishedToHandlers.Add(new EventPublishedToHandlerEntity(handlerIdentifier));
            return context.SaveAsync();
        }

        public Task ClearAsync()
        {
            foreach (UnPublishedEventEntity entity in context.UnPublishedEvents)
                context.UnPublishedEvents.Remove(entity);

            return context.SaveAsync();
        }
    }
}
