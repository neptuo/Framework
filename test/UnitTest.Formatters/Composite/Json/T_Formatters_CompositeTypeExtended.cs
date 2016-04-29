using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo;
using Neptuo.Activators;
using Neptuo.Formatters;
using Neptuo.Formatters.Converters;
using Neptuo.Formatters.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Formatters.Composite.Json
{
    [TestClass]
    public class T_Formatters_CompositeTypeExtended
    {
        [TestMethod]
        public void ExtendedObject()
        {
            Converts.Repository
                .AddJsonEnumSearchHandler()
                .AddJsonPrimitivesSearchHandler()
                .AddJsonObjectSearchHandler();

            Envelope<UserModel> envelope = Envelope.Create(new UserModel("Jon", "Doe"))
                .AddDelay(TimeSpan.FromMinutes(5))
                .AddSourceID("X5");

            envelope.Body.Guid = "ABCDEFG";

            ExtendedCompositeTypeFormatter formatter = new ExtendedCompositeTypeFormatter(
                new ReflectionCompositeTypeProvider(new ReflectionCompositeDelegateFactory()),
                new GetterFactory<ICompositeStorage>(() => new JsonCompositeStorage())
            );

            string json = formatter.Serialize(envelope.Body);
            UserModel model = formatter.Deserialize<UserModel>(json);

            Assert.AreEqual("ABCDEFG", model.Guid);
            Assert.AreEqual("Jon", model.UserName);
            Assert.AreEqual("Doe", model.Password);
        }
    }
}
