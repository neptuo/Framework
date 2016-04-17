using Neptuo.Data;
using Neptuo.Events;
using Neptuo.Events.Handlers;
using Neptuo.Models.Domains;
using Neptuo.Models.Keys;
using Neptuo.Models.Repositories;
using Orders.Domains.Events;
using System;
using System.Collections.Generic;
using System.Linq;
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

}
