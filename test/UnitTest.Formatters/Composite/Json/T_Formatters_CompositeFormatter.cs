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
        [TestMethod]
        public void Base()
        {
            Converts.Repository
                .AddJsonEnumSearchHandler()
                .AddJsonPrimitivesSearchHandler()
                .AddJsonObjectSearchHandler();

            UserModel model = new UserModel("John", "Doe");
            CompositeTypeFormatter formatter = new CompositeTypeFormatter(new ReflectionCompositeTypeProvider(new ReflectionCompositeDelegateFactory()), new DefaultFactory<JsonCompositeStorage>());

            using (MemoryStream stream = new MemoryStream())
            {
                Task<bool> isSerializedTask = formatter.TrySerializeAsync(model, new DefaultSerializerContext(typeof(UserModel), stream));
                isSerializedTask.Wait();

                Assert.AreEqual(true, isSerializedTask.Result);

                stream.Seek(0, SeekOrigin.Begin);

                string json = null;
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8, true, 1024, true))
                    json = reader.ReadToEnd();

                stream.Seek(0, SeekOrigin.Begin);

                IDeserializerContext context = new DefaultDeserializerContext(typeof(UserModel));
                Task<bool> isDeserializedTask = formatter.TryDeserializeAsync(stream, context);
                isDeserializedTask.Wait();

                Assert.AreEqual(true, isDeserializedTask.Result);
                model = (UserModel)context.Output;
            }

            Assert.IsNotNull(model);
        }
    }
}
