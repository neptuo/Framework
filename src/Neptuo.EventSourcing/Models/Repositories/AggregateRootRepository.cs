using Neptuo.Activators;
using Neptuo.Data;
using Neptuo.Events;
using Neptuo.Formatters;
using Neptuo.Internals;
using Neptuo.Models.Domains;
using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Repositories
{
    /// <summary>
    /// The implementation of EventSourcing AggregateRoot repository.
    /// </summary>
    /// <typeparam name="T">The type of the aggregate root.</typeparam>
    public class AggregateRootRepository<T> : IRepository<T, IKey>
        where T : AggregateRoot
    {
        private readonly IEventStore store;
        private readonly IFormatter formatter;
        private readonly IAggregateRootFactory<T> factory;
        private readonly IEventDispatcher eventDispatcher;

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="store">The underlaying event store.</param>
        /// <param name="formatter">The formatter for serializing and deserializing event payloads.</param>
        /// <param name="factory">The aggregate root factory.</param>
        /// <param name="eventDispatcher">The dispatcher for newly created events in the aggregates.</param>
        public AggregateRootRepository(IEventStore store, IFormatter formatter, IAggregateRootFactory<T> factory, IEventDispatcher eventDispatcher)
        {
            Ensure.NotNull(store, "store");
            Ensure.NotNull(formatter, "formatter");
            Ensure.NotNull(factory, "factory");
            Ensure.NotNull(eventDispatcher, "eventDispatcher");
            this.store = store;
            this.formatter = formatter;
            this.factory = factory;
            this.eventDispatcher = eventDispatcher;
        }

        public virtual void Save(T model)
        {
            Ensure.NotNull(model, "model");

            IEnumerable<IEvent> events = model.Events;
            if (events.Any())
            {
                IEnumerable<EventModel> eventModels = events.Select(e => new EventModel(e.AggregateKey, e.Key, formatter.SerializeEvent(e), e.Version));
                store.Save(eventModels);

                // TODO: Use snapshots.

                foreach (IEvent e in events)
                    eventDispatcher.PublishAsync(e).Wait();
            }
        }

        public T Find(IKey key)
        {
            Ensure.Condition.NotEmptyKey(key);

            // TODO: Use snapshots. The IEventStore should have method for Get-ing events with base-version.

            IEnumerable<EventModel> eventModels = store.Get(key);
            IEnumerable<object> events = eventModels.Select(e => formatter.DeserializeEvent(Type.GetType(e.EventKey.Type), e.Payload));
            
            T instance = factory.Create(key, events);
            return instance;
        }
    }
}
