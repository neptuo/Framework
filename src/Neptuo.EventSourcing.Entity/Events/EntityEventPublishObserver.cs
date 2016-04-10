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

        public void OnPublish(IKey eventKey, string handlerIdentifier)
        {
            UnPublishedEventEntity entity = context.UnPublishedEvents.FirstOrDefault(e => e.Event.ID == eventKey);
            if (entity == null)
                return;

            entity.PublishedToHandlers.Add(new PublishedToHandlerEntity(handlerIdentifier));
            context.SaveChanges();
        }
    }
}
