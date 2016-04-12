using Neptuo.Data.Entity;
using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Events
{
    public class EntityEventPublishObserver : IEventPublishObserver
    {
        private readonly IEventContext context;

        public EntityEventPublishObserver(IEventContext context)
        {
            Ensure.NotNull(context, "context");
            this.context = context;
        }

        public Task OnPublishAsync(IKey key, string handlerIdentifier)
        {
            GuidKey eventKey = key as GuidKey;
            if (eventKey == null)
                throw Ensure.Exception.NotGuidKey(eventKey.GetType(), "key");

            UnPublishedEventEntity entity = context.UnPublishedEvents.FirstOrDefault(e => e.Event.Type == eventKey.Type && e.Event.ID == eventKey.Guid);
            if (entity == null)
                return Task.FromResult(true);

            entity.PublishedToHandlers.Add(new PublishedToHandlerEntity(handlerIdentifier));
            context.SaveChanges();
            return Task.FromResult(true);
        }
    }
}
