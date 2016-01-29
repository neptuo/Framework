using Neptuo.Data;
using Neptuo.Events;
using Neptuo.Events.Handlers;
using Neptuo.EventSourcing.Events;
using Neptuo.Models.Domains;
using Neptuo.Models.Keys;
using Neptuo.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.EventSourcing
{
    namespace Events
    {
        public class OrderPlaced : Event
        { }

        public class OrderItemAdded : Event
        {
            public IKey ProductKey { get; private set; }
            public int Count { get; private set; }

            public OrderItemAdded(IKey productKey, int count)
            {
                Ensure.Condition.NotEmptyKey(productKey, "productKey");
                Ensure.Positive(count, "count");
                ProductKey = productKey;
                Count = count;
            }
        }

        public class OrderItemExtended : Event
        {
            public IKey ProductKey { get; private set; }
            public int Count { get; private set; }

            public OrderItemExtended(IKey productKey, int count)
            {
                Ensure.Condition.NotEmptyKey(productKey, "productKey");
                Ensure.Positive(count, "count");
                ProductKey = productKey;
                Count = count;
            }
        }

        public class OrderTotalRecalculated : Event
        {
            public decimal TotalPrice { get; private set; }

            public OrderTotalRecalculated(decimal totalPrice)
            {
                TotalPrice = totalPrice;
            }
        }
    }

    public class Order : AggregateRoot, 
        IEventHandler<OrderPlaced>,
        IEventHandler<OrderItemAdded>, 
        IEventHandler<OrderItemExtended>, 
        IEventHandler<OrderTotalRecalculated>
    {
        private readonly List<OrderItem> items = new List<OrderItem>();
        private decimal totalPrice = 0;

        public Order()
        {
            Publish(new OrderPlaced());
        }

        public Order(IKey key, IEnumerable<IEvent> events)
            : base(key, events)
        { }

        public void AddItem(IKey productKey, int count)
        {
            Ensure.Condition.NotEmptyKey(productKey, "productKey");
            Ensure.Positive(count, "count");

            OrderItem existingItem = items.FirstOrDefault(i => i.ProductKey == productKey);
            if (existingItem == null)
                Publish(new OrderItemAdded(productKey, count));
            else
                Publish(new OrderItemExtended(productKey, count));

            RecalculatePrice();
        }

        private void RecalculatePrice()
        {
            Publish(new OrderTotalRecalculated(items.Count() * items.Sum(i => i.Count) * 100));
        }

        #region Rebuilding state from events

        Task IEventHandler<OrderPlaced>.HandleAsync(OrderPlaced payload)
        {
            return Task.FromResult(true);
        }

        Task IEventHandler<OrderItemAdded>.HandleAsync(OrderItemAdded payload)
        {
            items.Add(new OrderItem(payload.ProductKey, payload.Count));
            return Task.FromResult(true);
        }

        Task IEventHandler<OrderItemExtended>.HandleAsync(OrderItemExtended payload)
        {
            OrderItem item = items.First(i => i.ProductKey == payload.ProductKey);
            item.Extend(payload.Count);
            return Task.FromResult(true);
        }

        Task IEventHandler<OrderTotalRecalculated>.HandleAsync(OrderTotalRecalculated payload)
        {
            totalPrice = payload.TotalPrice;
            return Task.FromResult(true);
        }

        #endregion
    }

    public class OrderItem 
    {
        public IKey ProductKey { get; private set; }
        public int Count { get; private set; }

        public OrderItem(IKey productKey, int count)
        {
            Ensure.Condition.NotEmptyKey(productKey, "productKey");
            Ensure.Positive(count, "count");
            ProductKey = productKey;
            Count = count;
        }

        public void Extend(int count)
        {
            Count += count;
        }
    }

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

}
