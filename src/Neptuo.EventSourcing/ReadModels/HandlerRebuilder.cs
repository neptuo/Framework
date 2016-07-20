using Neptuo.Data;
using Neptuo.Events;
using Neptuo.Events.Handlers;
using Neptuo.Formatters;
using Neptuo.Internals;
using Neptuo.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ReadModels
{
    /// <summary>
    /// Service pro rebuilding read-model from existing store.
    /// </summary>
    public class HandlerRebuilder
    {
        private readonly PersistentEventDispatcher eventDispatcher;
        private readonly IEventRebuilderStore store;
        private readonly IDeserializer deserializer;

        /// <summary>
        /// Creates new instance for rebuilding events from <paramref name="store"/> using <paramref name="deserializer"/>
        /// for loading event instances.
        /// </summary>
        /// <param name="store">The store containing already published events.</param>
        /// <param name="deserializer">The deserializer from loading event instances.</param>
        public HandlerRebuilder(IEventRebuilderStore store, IDeserializer deserializer)
        {
            Ensure.NotNull(store, "store");
            Ensure.NotNull(deserializer, "deserializer");
            this.eventDispatcher = new PersistentEventDispatcher(new EmptyEventStore());
            this.store = store;
            this.deserializer = deserializer;
        }

        public HandlerRebuilder(IEventRebuilderStore store, IDeserializer deserializer, ISchedulingProvider schedulingProvider)
        {
            Ensure.NotNull(store, "store");
            Ensure.NotNull(deserializer, "deserializer");
            this.eventDispatcher = new PersistentEventDispatcher(new EmptyEventStore(), schedulingProvider);
            this.store = store;
            this.deserializer = deserializer;
        }

        /// <summary>
        /// Pushlishes all events from store on <paramref name="handler"/>.
        /// </summary>
        /// <param name="handler">The handler to replay events on.</param>
        /// <returns>Continuation task.</returns>
        public async Task RunAsync(object handler)
        {
            Ensure.NotNull(handler, "handler");

            // 1) Find all handlers.
            eventDispatcher.Handlers.AddAll(handler);

            // 2) Use list of required event types to load events from store.
            IEnumerable<string> eventTypes = eventDispatcher
                .EnumerateEventTypes()
                .Select(t => t.AssemblyQualifiedName);

            IEnumerable<EventModel> eventData = await store.GetAsync(eventTypes);
            IEnumerable<IEvent> events = eventData.Select(e => deserializer.DeserializeEvent(Type.GetType(e.EventKey.Type), e.Payload));

            // 3) Replay events on handler.
            foreach (IEvent payload in events)
                await eventDispatcher.PublishAsync(payload);
        }
    }
}
