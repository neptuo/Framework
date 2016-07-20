using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Activators;
using Neptuo.Data;
using Neptuo.Formatters;
using Neptuo.Formatters.Converters;
using Neptuo.Formatters.Metadata;
using Neptuo.Models.Keys;
using Neptuo.ReadModels;
using Orders.Domains;
using Orders.Domains.Events;
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
                Factory.Getter(() => new JsonCompositeStorage())
            );
            MockEventStore eventStore = new MockEventStore();

            // Creation of state.
            Order order = new Order(KeyFactory.Create(typeof(Order)));
            order.AddItem(KeyFactory.Create(typeof(T_EventSourcing_ReadModels_Rebuilder)), 2);
            order.AddItem(KeyFactory.Create(typeof(HandlerRebuilder)), 5);
            eventStore.Save(order.Events.Select(e => new EventModel(order.Key, e.Key, formatter.Serialize(e), e.Version)));

            // Rebuilding model.
            HandlerRebuilder rebuilder = new HandlerRebuilder(eventStore, formatter);
            ReadModelHandler handler = new ReadModelHandler();
            rebuilder.RunAsync(handler).Wait();

            Assert.AreEqual(1, handler.Totals.Count);
            Assert.AreEqual(
                order.Events.OfType<OrderTotalRecalculated>().Last().TotalPrice, 
                handler.Totals.FirstOrDefault().Value
            );
        }
    }
}
