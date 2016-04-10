using Neptuo.Activators;
using Neptuo.Data;
using Neptuo.Events;
using Neptuo.Formatters;
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
    /// <typeparam name="T"></typeparam>
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

        public void Save(T model)
        {
            Ensure.NotNull(model, "model");

            IEnumerable<IEvent> events = model.Events;
            if (events.Any())
            {
                IEnumerable<EventModel> eventModels = events.Select(e => new EventModel(e.AggregateKey, e.Key, SerializeEvent(e)));
                store.Save(eventModels);
            }

            IEnumerable<Task> tasks = events.Select(e => eventDispatcher.PublishAsync(e));
            Task.WaitAll(tasks.ToArray());
        }

        private string SerializeEvent(IEvent payload)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                Task<bool> result = formatter.TrySerializeAsync(payload, new DefaultSerializerContext(payload.GetType(), stream));
                if (!result.IsCompleted)
                    result.Wait();

                if (result.Result)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    return Encoding.UTF8.GetString(stream.ToArray());
                }
            }

            throw Ensure.Exception.NotImplemented();
        }

        public T Find(IKey key)
        {
            Ensure.Condition.NotEmptyKey(key, "key");

            IEnumerable<EventModel> eventModels = store.Get(key);
            IEnumerable<object> events = eventModels.Select(e => DeserializeEvent(Type.GetType(e.EventKey.Type), e.Payload));
            
            T instance = factory.Create(key, events);
            return instance;
        }

        private IEvent DeserializeEvent(Type eventType, string payload)
        {
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(payload)))
            {
                DefaultDeserializerContext context = new DefaultDeserializerContext(eventType);
                Task<bool> result = formatter.TryDeserializeAsync(stream, context);
                if (!result.IsCompleted)
                    result.Wait();

                if (result.Result)
                    return (IEvent)context.Output;
            }

            throw Ensure.Exception.NotImplemented();
        }
    }
}
