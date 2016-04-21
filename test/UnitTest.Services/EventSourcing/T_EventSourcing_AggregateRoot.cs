using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Activators;
using Neptuo.Commands;
using Neptuo.Data;
using Neptuo.Data.Entity;
using Neptuo.Events;
using Neptuo.Formatters;
using Neptuo.Models.Domains;
using Neptuo.Models.Keys;
using Neptuo.Models.Repositories;
using Orders.Domains;
using Orders.Domains.Commands;
using Orders.Domains.Commands.Handlers;
using Orders.Domains.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.EventSourcing
{
    [TestClass]
    public class T_EventSourcing_AggregateRoot
    {
        [TestMethod]
        public void RegisteringHandlers()
        {
            Order order1 = new Order(KeyFactory.Create(typeof(Order)));
            order1.AddItem(GuidKey.Create(Guid.NewGuid(), "Product"), 5);

            IEnumerator<object> eventEnumerator = order1.Events.GetEnumerator();
            Assert.AreEqual(true, eventEnumerator.MoveNext());
            Assert.AreEqual(typeof(OrderPlaced), eventEnumerator.Current.GetType());
            Assert.AreEqual(true, eventEnumerator.MoveNext());
            Assert.AreEqual(typeof(OrderItemAdded), eventEnumerator.Current.GetType());
            Assert.AreEqual(true, eventEnumerator.MoveNext());
            Assert.AreEqual(typeof(OrderTotalRecalculated), eventEnumerator.Current.GetType());
            Assert.AreEqual(false, eventEnumerator.MoveNext());
        }

        [TestMethod]
        public void SaveAndLoadWithMockRepository()
        {
            MockEventStore eventStore = new MockEventStore();

            AggregateRootRepository<Order> repository = new AggregateRootRepository<Order>(
                eventStore, 
                new JsonFormatter(),
                new ReflectionAggregateRootFactory<Order>(),
                new DefaultEventManager()
            );

            Order order = new Order(KeyFactory.Create(typeof(Order)));
            order.AddItem(GuidKey.Create(Guid.NewGuid(), "Product"), 5);

            repository.Save(order);

            IEnumerable<EventModel> serializedEvents = eventStore.Get(order.Key);
            Assert.AreEqual(3, serializedEvents.Count());
        }

        [TestMethod]
        public void SaveAndLoadWithEntityRepository()
        {
            EventSourcingContext context = new EventSourcingContext(@"Data Source=localhost; Initial Catalog=EventStore;Integrated Security=SSPI");
            EntityEventStore eventStore = new EntityEventStore(context);

            AggregateRootRepository<Order> repository = new AggregateRootRepository<Order>(
                eventStore,
                new JsonFormatter(),
                new ReflectionAggregateRootFactory<Order>(),
                new PersistentEventDispatcher(eventStore)
            );

            PersistentCommandDispatcher commandDispatcher = new PersistentCommandDispatcher(
                new SerialCommandDistributor(),
                new EntityCommandStore(context),
                new JsonFormatter()
            );

            CreateOrderHandler createHandler = new CreateOrderHandler(repository);
            AddOrderItemHandler addItemHandler = new AddOrderItemHandler(repository);
            commandDispatcher
                .Add<CreateOrder>(createHandler)
                .Add<AddOrderItem>(addItemHandler);

            CreateOrder create = new CreateOrder();
            commandDispatcher.HandleAsync(create).Wait();

            AddOrderItem addItem = new AddOrderItem(create.OrderKey, GuidKey.Create(Guid.NewGuid(), "Product"), 5);
            commandDispatcher.HandleAsync(addItem).Wait();

            IEnumerable<EventModel> serializedEvents = eventStore.Get(create.Key);
            Assert.AreEqual(3, serializedEvents.Count());
        }
    }
}
