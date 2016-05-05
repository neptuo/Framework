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

    public interface IHelloService
    {
        string SayHello(string name);
    }

    public class HiHelloService : IHelloService
    {
        public string SayHello(string name)
        {
            return String.Format("Hi, {0}!");
        }
    }

    public class AggregateWithParameters : AggregateRoot
    {
        public IHelloService Service { get; private set; }

        public AggregateWithParameters(IHelloService service)
        {
            Ensure.NotNull(service, "service");
            Service = service;
        }

        public AggregateWithParameters(IKey key, IEnumerable<IEvent> events)
            : base(key, events)
        { }

        public AggregateWithParameters(IKey key, IEnumerable<IEvent> events, IHelloService service)
            : base(key, events)
        {
            Ensure.NotNull(service, "service");
            Service = service;
        }
    }
}
