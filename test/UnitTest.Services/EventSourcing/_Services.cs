using Neptuo.Activators;
using Neptuo.Collections.Specialized;
using Neptuo.Commands;
using Neptuo.Data;
using Neptuo.Events;
using Neptuo.Events.Handlers;
using Neptuo.Formatters;
using Neptuo.Formatters.Metadata;
using Neptuo.Models.Domains;
using Neptuo.Models.Keys;
using Neptuo.Models.Repositories;
using Orders.Domains.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.EventSourcing
{
    public class MockEventStore : IEventStore
    {
        private readonly Dictionary<IKey, List<EventModel>> storage = new Dictionary<IKey, List<EventModel>>();

        public IEnumerable<EventModel> Get(IKey aggregateKey)
        {
            List<EventModel> events;
            if (storage.TryGetValue(aggregateKey, out events))
                return events;

            return Enumerable.Empty<EventModel>();
        }

        public void Save(IEnumerable<EventModel> events)
        {
            EventModel payload = events.FirstOrDefault();
            if (payload != null)
            {
                List<EventModel> entities;
                if (!storage.TryGetValue(payload.AggregateKey, out entities))
                    storage[payload.AggregateKey] = entities = new List<EventModel>();

                entities.AddRange(events);
            }
        }
    }

    public class OrderPlacedHandler : IEventHandler<OrderPlaced>
    {
        public Task HandleAsync(OrderPlaced payload)
        {
            Console.WriteLine(payload.AggregateKey);
            return Task.FromResult(true);
        }
    }


    public class ExtendedComposityTypeFormatter : CompositeTypeFormatter
    {
        private readonly EventExtender events = new EventExtender();
        private readonly CommandExtender commands = new CommandExtender();

        public const string MetadataKey = "Metadata";

        public ExtendedComposityTypeFormatter()
            : base(new ReflectionCompositeTypeProvider(new ReflectionCompositeDelegateFactory(), BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public), new DefaultFactory<JsonCompositeStorage>())
        { }

        protected override bool TryLoad(Stream input, IDeserializerContext context, CompositeType type, ICompositeStorage storage)
        {
            if (base.TryLoad(input, context, type, storage))
            {
                Event payload = context.Output as Event;
                if (payload != null)
                {
                    events.Load(GetOrAddPayloadStorage(storage), payload);
                }
                else
                {
                    Command command = context.Output as Command;
                    if (command != null)
                        commands.Load(GetOrAddPayloadStorage(storage), command);
                }

                IKeyValueCollection metadata;
                ICompositeStorage metadataStorage;
                if (context.TryGetEnvelopeMetadata(out metadata) && storage.TryGet(MetadataKey, out metadataStorage))
                {
                    foreach (string key in metadataStorage.Keys)
                        metadata.Add(key, metadataStorage.Get<object>(key));
                }

                return true;
            }

            return false;
        }

        protected override bool TryStore(object input, ISerializerContext context, CompositeType type, CompositeVersion typeVersion, ICompositeStorage storage)
        {
            if (base.TryStore(input, context, type, typeVersion, storage))
            {
                Event payload = input as Event;
                if (payload != null)
                {
                    events.Store(GetOrAddPayloadStorage(storage), payload);
                }
                else
                {
                    Command command = input as Command;
                    if (command != null)
                        commands.Store(GetOrAddPayloadStorage(storage), command);
                }

                IReadOnlyKeyValueCollection metadata;
                if (context.TryGetEnvelopeMetadata(out metadata))
                {
                    ICompositeStorage metadataStorage = storage.Add(MetadataKey);
                    foreach (string key in metadata.Keys)
                        metadataStorage.Add(key, metadata.Get<object>(key));
                }

                return true;
            }

            return false;
        }

        private ICompositeStorage GetOrAddPayloadStorage(ICompositeStorage storage)
        {
            ICompositeStorage payloadStorage;
            if (storage.TryGet(CompositeTypeFormatter.Name.Payload, out payloadStorage))
                return payloadStorage;

            return storage.Add(CompositeTypeFormatter.Name.Payload);
        }
    }
}
