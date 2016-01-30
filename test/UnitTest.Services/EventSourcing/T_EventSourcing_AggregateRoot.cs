using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Activators;
using Neptuo.Data;
using Neptuo.Data.Entity;
using Neptuo.EventSourcing.Events;
using Neptuo.Formatters;
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
    [TestClass]
    public class T_EventSourcing_AggregateRoot
    {
        [TestMethod]
        public void RegisteringHandlers()
        {
            Order order1 = new Order();
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
                new ReflectionAggregateRootFactory<Order>()
            );

            Order order = new Order();
            order.AddItem(GuidKey.Create(Guid.NewGuid(), "Product"), 5);

            repository.Save(order);

            IEnumerable<EventModel> serializedEvents = eventStore.Get(order.Key);
            Assert.AreEqual(3, serializedEvents.Count());
        }

        [TestMethod]
        public void SaveAndLoadWithEntityRepository()
        {
            EntityEventStore eventStore = new EntityEventStore(new EventContext(@"Data Source=.\SQLEXPRESS; Initial Catalog=EventStore;Integrated Security=SSPI"));

            AggregateRootRepository<Order> repository = new AggregateRootRepository<Order>(
                eventStore,
                new JsonFormatter(),
                new ReflectionAggregateRootFactory<Order>()
            );

            Order order = new Order();
            order.AddItem(GuidKey.Create(Guid.NewGuid(), "Product"), 5);

            repository.Save(order);

            IEnumerable<EventModel> serializedEvents = eventStore.Get(order.Key);
            Assert.AreEqual(3, serializedEvents.Count());
        }
    }
}
