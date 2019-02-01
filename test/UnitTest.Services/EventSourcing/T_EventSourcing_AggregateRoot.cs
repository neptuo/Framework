using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Activators;
using Neptuo.Commands;
using Neptuo.Data;
using Neptuo.Data.Entity;
using Neptuo.Events;
using Neptuo.Events.Handlers;
using Neptuo.Formatters;
using Neptuo.Formatters.Converters;
using Neptuo.Models.Keys;
using Neptuo.Models.Repositories;
using Neptuo.Models.Snapshots;
using Orders;
using Orders.Commands;
using Orders.Commands.Handlers;
using Orders.Events;
using Orders.Snapshots;
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
        public void RegisteringInterfaceHandlers()
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
        public void RegisteringConventionHandlers()
        {
            Product product1 = new Product("Socks");
            product1.ChangeName("T-shirt");

            IEnumerator<object> eventEnumerator = product1.Events.GetEnumerator();
            Assert.AreEqual(true, eventEnumerator.MoveNext());
            Assert.AreEqual(typeof(ProductCreated), eventEnumerator.Current.GetType());
            Assert.AreEqual(true, eventEnumerator.MoveNext());
            Assert.AreEqual(typeof(ProductNameChanged), eventEnumerator.Current.GetType());
            Assert.AreEqual(false, eventEnumerator.MoveNext());
        }

        [TestMethod]
        public void SaveAndLoadWithMockRepository()
        {
            Converts.Repository
                .AddJsonKey()
                .AddJsonEnumSearchHandler()
                .AddJsonPrimitivesSearchHandler()
                .AddJsonObjectSearchHandler();

            ICompositeTypeProvider compositeTypeProvider = new ReflectionCompositeTypeProvider(new ReflectionCompositeDelegateFactory());
            IFactory<ICompositeStorage> storageFactory = Factory.Default<JsonCompositeStorage>();

            MockEventStore eventStore = new MockEventStore();

            PersistentEventDispatcher eventDispatcher = new PersistentEventDispatcher(new EmptyEventStore());

            AggregateRootRepository<Order> repository = new AggregateRootRepository<Order>(
                eventStore,
                new CompositeEventFormatter(compositeTypeProvider, storageFactory),
                new ReflectionAggregateRootFactory<Order>(),
                eventDispatcher,
                new OrderSnapshotProvider(),
                new MockSnapshotStore()
            );

            PersistentCommandDispatcher commandDispatcher = new PersistentCommandDispatcher(
                new SerialCommandDistributor(),
                new EmptyCommandStore(),
                new CompositeCommandFormatter(compositeTypeProvider, storageFactory)
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
            commandDispatcher.HandleAsync(Envelope.Create(addItem).AddDelay(TimeSpan.FromMinutes(1)));

            Task<OrderTotalRecalculated> task = eventDispatcher.Handlers.Await<OrderTotalRecalculated>();
            task.Wait();
            Console.WriteLine(task.Result);

            serializedEvents = eventStore.Get(create.OrderKey).ToList();
            Assert.AreEqual(4, serializedEvents.Count());
        }

        [TestMethod]
        public void SaveAndLoadWithEntityRepository()
        {
            Converts.Repository
                .AddJsonKey()
                .AddJsonEnumSearchHandler()
                .AddJsonPrimitivesSearchHandler()
                .AddJsonObjectSearchHandler();

            ICompositeTypeProvider compositeTypeProvider = new ReflectionCompositeTypeProvider(new ReflectionCompositeDelegateFactory());
            IFactory<ICompositeStorage> storageFactory = new GetterFactory<ICompositeStorage>(() => new JsonCompositeStorage());

            EventSourcingContext context = new EventSourcingContext(@"Data Source=.\sqlexpress; Initial Catalog=EventStore;Integrated Security=SSPI");
            EntityEventStore eventStore = new EntityEventStore(context);

            PersistentEventDispatcher eventDispatcher = new PersistentEventDispatcher(eventStore);

            AggregateRootRepository<Order> repository = new AggregateRootRepository<Order>(
                eventStore,
                new CompositeEventFormatter(compositeTypeProvider, storageFactory),
                new ReflectionAggregateRootFactory<Order>(),
                eventDispatcher,
                new NoSnapshotProvider(),
                new EmptySnapshotStore()
            );

            PersistentCommandDispatcher commandDispatcher = new PersistentCommandDispatcher(
                new SerialCommandDistributor(),
                new EntityCommandStore(context),
                new CompositeCommandFormatter(compositeTypeProvider, storageFactory)
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
            commandDispatcher.HandleAsync(Envelope.Create(addItem).AddDelay(TimeSpan.FromMinutes(1)));

            Task<OrderTotalRecalculated> task = eventDispatcher.Handlers.Await<OrderTotalRecalculated>();
            task.Wait();
            Console.WriteLine(task.Result);

            serializedEvents = eventStore.Get(create.OrderKey).ToList();
            Assert.AreEqual(4, serializedEvents.Count());
        }
    }
}
