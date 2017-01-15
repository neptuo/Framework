using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo;
using Neptuo.Activators;
using Neptuo.Formatters;
using Neptuo.Formatters.Converters;
using Neptuo.Formatters.Metadata;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Formatters.Composite.Json
{
    [TestClass]
    public class T_Formatters_CompositeFormatter
    {
        private CompositeTypeFormatter formatter = new CompositeTypeFormatter(new ReflectionCompositeTypeProvider(new ReflectionCompositeDelegateFactory()), new DefaultFactory<JsonCompositeStorage>());

        [TestMethod]
        public void Base()
        {
            Converts.Repository
                .AddJsonEnumSearchHandler()
                .AddJsonPrimitivesSearchHandler()
                .AddJsonObjectSearchHandler();

            UserModel model = new UserModel("John", "Doe");

            string json = formatter.Serialize(model);
            Assert.IsNotNull(json);

            model = formatter.Deserialize<UserModel>(json);
            Assert.IsNotNull(model);
        }

        [TestMethod]
        public void NotSupportedValueException()
        {
            CompositeObject model = new CompositeObject(new SingleModel("Doe"));

            string json = formatter.Serialize(model);
            Assert.IsNotNull(json);
        }
    }
}
