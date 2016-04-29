using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Activators;
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

            Envelope<CreateOrder> envelope = new Envelope<CreateOrder>(new CreateOrder())
                .AddDelay(delay)
                .AddSourceID(sourceID);

            string json = formatter.Serialize(envelope);
            envelope = formatter.Deserialize<Envelope<CreateOrder>>(json);

            Assert.AreEqual(delay, envelope.GetDelay());
            Assert.AreEqual(sourceID, envelope.GetSourceID());
        }
    }
}
