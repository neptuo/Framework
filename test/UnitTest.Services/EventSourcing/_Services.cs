using Neptuo.Events.Handlers;
using Neptuo.EventSourcing.Events;
using Neptuo.Models.Domains;
using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.EventSourcing
{
    namespace Events
    {
        public class OrderPlaced
        {
            public IKey OrderKey { get; private set; }

            public OrderPlaced(IKey orderKey)
            {
                Ensure.Condition.NotEmptyKey(orderKey, "orderKey");
                OrderKey = orderKey;
            }
        }

        public class OrderItemAdded
        {
            public IKey OrderKey { get; private set; }
            public IKey ProductKey { get; private set; }
            public int Count { get; private set; }

            public OrderItemAdded(IKey orderKey, IKey productKey, int count)
            {
                Ensure.Condition.NotEmptyKey(orderKey, "orderKey");
                Ensure.Condition.NotEmptyKey(productKey, "productKey");
                Ensure.Positive(count, "count");
                OrderKey = orderKey;
                ProductKey = productKey;
                Count = count;
            }
        }

        public class OrderItemExtended
        {
            public IKey OrderKey { get; private set; }
            public IKey ProductKey { get; private set; }
            public int Count { get; private set; }

            public OrderItemExtended(IKey orderKey, IKey productKey, int count)
            {
                Ensure.Condition.NotEmptyKey(orderKey, "orderKey");
                Ensure.Condition.NotEmptyKey(productKey, "productKey");
                Ensure.Positive(count, "count");
                OrderKey = orderKey;
                ProductKey = productKey;
                Count = count;
            }
        }

        public class OrderTotalRecalculated
        {
            public IKey OrderKey { get; private set; }
            public decimal TotalPrice {get; private set;}

            public OrderTotalRecalculated(IKey orderKey, decimal totalPrice)
            {
                Ensure.Condition.NotEmptyKey(orderKey, "orderKey");
                OrderKey = orderKey;
                TotalPrice = totalPrice;
            }
        }
    }

    public class Order : AggregateRoot, 
        IEventHandler<OrderItemAdded>, 
        IEventHandler<OrderItemExtended>, 
        IEventHandler<OrderTotalRecalculated>
    {
        private readonly List<OrderItem> items = new List<OrderItem>();
        private decimal totalPrice = 0;

        public Order()
        { }

        public Order(StringKey key, IEnumerable<object> events)
            : base(key, events)
        {
            Publish(new OrderPlaced(Key));
        }

        public void AddItem(IKey productKey, int count)
        {
            Ensure.Condition.NotEmptyKey(productKey, "productKey");
            Ensure.Positive(count, "count");

            OrderItem existingItem = items.FirstOrDefault(i => i.ProductKey == productKey);
            if (existingItem == null)
                Publish(new OrderItemExtended(Key, productKey, count));
            else
                Publish(new OrderItemAdded(Key, productKey, count));

            RecalculatePrice();
        }

        private void RecalculatePrice()
        {
            Publish(new OrderTotalRecalculated(Key, items.Count() * items.Sum(i => i.Count) * 100));
        }

        #region Rebuilding state from events

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
}
