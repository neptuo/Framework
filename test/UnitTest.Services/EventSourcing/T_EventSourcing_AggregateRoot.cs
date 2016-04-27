using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Activators;
using Neptuo.Commands;
using Neptuo.Data;
using Neptuo.Data.Entity;
using Neptuo.Events;
using Neptuo.Events.Handlers;
using Neptuo.Formatters;
using Neptuo.Formatters.Converters;
using Neptuo.Formatters.Metadata;
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
            Converts.Repository
                .AddJsonEnumSearchHandler()
                .AddJsonPrimitivesSearchHandler()
                .AddJsonObjectSearchHandler();

            CompositeTypeFormatter formatter = new CompositeTypeFormatter(
                new ReflectionCompositeTypeProvider(new ReflectionCompositeDelegateFactory()), 
                new GetterFactory<ICompositeStorage>(() => new JsonCompositeStorage())
            );
            MockEventStore eventStore = new MockEventStore();

            AggregateRootRepository<Order> repository = new AggregateRootRepository<Order>(
                eventStore,
                formatter,
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
            Converts.Repository
                .AddJsonEnumSearchHandler()
                .AddJsonPrimitivesSearchHandler()
                .AddJsonObjectSearchHandler();

            CompositeTypeFormatter formatter = new CompositeTypeFormatter(
                new ReflectionCompositeTypeProvider(new ReflectionCompositeDelegateFactory()),
                new GetterFactory<ICompositeStorage>(() => new JsonCompositeStorage())
            );
            EventSourcingContext context = new EventSourcingContext(@"Data Source=.\sqlexpress; Initial Catalog=EventStore;Integrated Security=SSPI");
            EntityEventStore eventStore = new EntityEventStore(context);

            PersistentEventDispatcher eventDispatcher = new PersistentEventDispatcher(eventStore);

            AggregateRootRepository<Order> repository = new AggregateRootRepository<Order>(
                eventStore,
                formatter,
                new ReflectionAggregateRootFactory<Order>(),
                eventDispatcher
            );

            PersistentCommandDispatcher commandDispatcher = new PersistentCommandDispatcher(
                new SerialCommandDistributor(),
                new EntityCommandStore(context),
                formatter
            );

            CreateOrderHandler createHandler = new CreateOrderHandler(repository);
            AddOrderItemHandler addItemHandler = new AddOrderItemHandler(repository);
            commandDispatcher.Handlers
                .Add<CreateOrder>(createHandler)
                .Add<AddOrderItem>(addItemHandler);

            CreateOrder create = new CreateOrder();
            commandDispatcher.HandleAsync(create);

            eventDispatcher.Handlers.Await<OrderPlaced>().Wait();

            IEnumerable<EventModel> serializedEvents = eventStore.Get(create.OrderKey).ToList();
            Assert.AreEqual(1, serializedEvents.Count());

            AddOrderItem addItem = new AddOrderItem(create.OrderKey, GuidKey.Create(Guid.NewGuid(), "Product"), 5);
            commandDispatcher.HandleAsync(Envelope.Create(addItem).AddDelay(TimeSpan.FromSeconds(2)));

            eventDispatcher.Handlers.Await<OrderTotalRecalculated>().Wait();

            serializedEvents = eventStore.Get(create.OrderKey).ToList();
            Assert.AreEqual(3, serializedEvents.Count());
        }
    }
}
