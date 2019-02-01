using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Activators;
using Neptuo.Data;
using Neptuo.Events;
using Neptuo.Formatters;
using Neptuo.Formatters.Converters;
using Neptuo.ReadModels;
using Orders;
using Orders.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.EventSourcing
{
    [TestClass]
    public class T_EventSourcing_ReadModels_Rebuilder
    {
        [TestMethod]
        public void Base()
        {
            Converts.Repository
                .AddJsonEnumSearchHandler()
                .AddJsonPrimitivesSearchHandler()
                .AddJsonObjectSearchHandler()
                .AddJsonKey()
                .AddJsonTimeSpan();

            CompositeEventFormatter formatter = new CompositeEventFormatter(
                new ReflectionCompositeTypeProvider(new ReflectionCompositeDelegateFactory()),
                Factory.Default<JsonCompositeStorage>()
            );
            MockEventStore eventStore = new MockEventStore();

            // Creation of state.
            Order order = new Order(KeyFactory.Create(typeof(Order)));
            order.AddItem(KeyFactory.Create(typeof(T_EventSourcing_ReadModels_Rebuilder)), 2);
            order.AddItem(KeyFactory.Create(typeof(Rebuilder)), 5);
            eventStore.Save(order.Events.Select(e => new EventModel(order.Key, e.Key, formatter.Serialize(e), e.Version)));

            // Rebuilding model.
            Rebuilder rebuilder = new Rebuilder(eventStore, formatter);
            ReadModelHandler handler = new ReadModelHandler();
            rebuilder.AddAll(handler);
            rebuilder.RunAsync().Wait();

            Assert.AreEqual(1, handler.Totals.Count);
            Assert.AreEqual(
                order.Events.OfType<OrderTotalRecalculated>().Last().TotalPrice, 
                handler.Totals.FirstOrDefault().Value
            );
        }
    }
}
