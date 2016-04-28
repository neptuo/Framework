using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Formatters;
using Neptuo.Formatters.Converters;
using Orders.Domains.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.EventSourcing
{
    [TestClass]
    public class T_EventSourcing_EnvelopeFormatter
    {
        [TestMethod]
        public void SerializeAndDeserializeWithMetadata()
        {
            Converts.Repository
                .AddJsonEnumSearchHandler()
                .AddJsonObjectSearchHandler()
                .AddJsonPrimitivesSearchHandler();

            IFormatter formatter = new EnvelopeFormatter(new ExtendedComposityTypeFormatter());

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
