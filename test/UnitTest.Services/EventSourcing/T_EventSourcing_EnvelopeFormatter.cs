using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Activators;
using Neptuo.Converters;
using Neptuo.Collections.Specialized;
using Neptuo.Formatters;
using Neptuo.Formatters.Converters;
using Neptuo.Formatters.Metadata;
using Orders.Domains.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Neptuo.Models.Keys;
using Orders.Domains.Events;
using Orders.Domains;

namespace Neptuo.EventSourcing
{
    [TestClass]
    public class T_EventSourcing_EnvelopeFormatter
    {
        [TestMethod]
        public void SerializeAndDeserializeCommandWithMetadata()
        {
            Converts.Repository
                .AddJsonEnumSearchHandler()
                .AddJsonObjectSearchHandler()
                .AddJsonPrimitivesSearchHandler()
                .AddJsonKey()
                .AddJsonTimeSpan();

            IFormatter formatter = new CompositeCommandFormatter(
                new ReflectionCompositeTypeProvider(
                    new ReflectionCompositeDelegateFactory(),
                    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public
                ),
                new DefaultFactory<JsonCompositeStorage>()
            );

            TimeSpan delay = TimeSpan.FromSeconds(50);
            string sourceID = "AbcDef";
            int value = 15;

            Envelope<CreateOrder> envelope = new Envelope<CreateOrder>(new CreateOrder())
                .AddDelay(delay)
                .AddSourceID(sourceID);

            IKey key = envelope.Body.Key;
            IKey orderKey = envelope.Body.OrderKey;

            envelope.Metadata.Add("Value", value);

            string json = formatter.Serialize(envelope);
            envelope = formatter.Deserialize<Envelope<CreateOrder>>(json);

            Assert.AreEqual(delay, envelope.GetDelay());
            Assert.AreEqual(sourceID, envelope.GetSourceID());
            Assert.AreEqual(value, envelope.Metadata.Get<int>("Value"));
            Assert.AreEqual(key, envelope.Body.Key);
            Assert.AreEqual(orderKey, envelope.Body.OrderKey);
        }

        [TestMethod]
        public void SerializeAndDeserializeEventWithMetadata()
        {
            Converts.Repository
                .AddJsonEnumSearchHandler()
                .AddJsonObjectSearchHandler()
                .AddJsonPrimitivesSearchHandler()
                .AddJsonKey()
                .AddJsonTimeSpan();

            IFormatter formatter = new CompositeEventFormatter(
                new ReflectionCompositeTypeProvider(
                    new ReflectionCompositeDelegateFactory(),
                    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public
                ),
                new DefaultFactory<JsonCompositeStorage>()
            );

            TimeSpan delay = TimeSpan.FromSeconds(50);
            string sourceID = "AbcDef";
            int value = 15;

            IKey orderKey = KeyFactory.Create(typeof(Order));
            Order order = new Order(orderKey);
            OrderPlaced payload = order.Events.OfType<OrderPlaced>().First();
            IKey key = payload.Key;

            Envelope<OrderPlaced> envelope = new Envelope<OrderPlaced>(payload)
                .AddDelay(delay)
                .AddSourceID(sourceID);

            envelope.Metadata.Add("Value", value);

            string json = formatter.Serialize(envelope);
            envelope = formatter.Deserialize<Envelope<OrderPlaced>>(json);

            Assert.AreEqual(delay, envelope.GetDelay());
            Assert.AreEqual(sourceID, envelope.GetSourceID());
            Assert.AreEqual(value, envelope.Metadata.Get<int>("Value"));
            Assert.AreEqual(key, envelope.Body.Key);
            Assert.AreEqual(orderKey, envelope.Body.AggregateKey);
        }
    }
}
