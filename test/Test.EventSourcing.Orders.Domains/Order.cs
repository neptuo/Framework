using Neptuo;
using Neptuo.Events;
using Neptuo.Events.Handlers;
using Neptuo.Models.Domains;
using Neptuo.Models.Keys;
using Neptuo.Models.Snapshots;
using Orders.Domains.Events;
using Orders.Domains.Snapshots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Domains
{
    public class Order : AggregateRoot,
        IEventHandler<OrderPlaced>,
        IEventHandler<OrderItemAdded>,
        IEventHandler<OrderItemExtended>,
        IEventHandler<OrderTotalRecalculated>,
        IEventHandler<OrderItemSummaryChanged>
    {
        private readonly List<OrderItem> items = new List<OrderItem>();
        private decimal totalPrice = 0;

        public IEnumerable<OrderItem> Items
        {
            get { return items; }
        }

        public decimal TotalPrice
        {
            get { return totalPrice; }
        }

        public Order(IKey key)
            : base(key)
        {
            Publish(new OrderPlaced());
        }

        public Order(IKey key, IEnumerable<IEvent> events)
            : base(key, events)
        { }

        public Order(IKey key, ISnapshot snapshot, IEnumerable<IEvent> events)
            : base(key, snapshot, events)
        { }

        protected override void LoadSnapshot(ISnapshot snapshot)
        {
            OrderSnapshot state = (OrderSnapshot)snapshot;
            items.AddRange(state.Items);
            totalPrice = state.TotalPrice;
        }

        public void AddItem(IKey productKey, int count)
        {
            Ensure.Condition.NotEmptyKey(productKey);
            Ensure.Positive(count, "count");

            OrderItem existingItem = items.FirstOrDefault(i => i.ProductKey == productKey);
            if (existingItem == null)
            {
                Publish(new OrderItemAdded(productKey, count));
                Publish(new OrderItemSummaryChanged(items.Select(i => i.ProductKey)));
            }
            else
            {
                Publish(new OrderItemExtended(productKey, count));
            }

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

        Task IEventHandler<OrderItemSummaryChanged>.HandleAsync(OrderItemSummaryChanged payload)
        {
            return Task.FromResult(true);
        }

        #endregion
    }
}
