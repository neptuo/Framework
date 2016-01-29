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

        [TestMethod]
        public void SimplePerf()
        {
            int count = 100000;
            Stopwatch sw = new Stopwatch();

            long newtonSoft = 0;
            long buildCompositeType = 0;
            long composite = 0;
            long compositeStorage = 0;

            // NEWTONSOFT.JSON DIRECTLY.
            sw.Restart();
            for (int i = 0; i < count; i++)
            {
                UserModel model = new UserModel(1, "UserName", "Password");
                string json = JsonConvert.SerializeObject(model);

                model = JsonConvert.DeserializeObject<UserModel>(json);
            }
            sw.Stop();
            newtonSoft = sw.ElapsedMilliseconds;


            // BUILD COMPOSITE TYPE.
            sw.Restart();
            ICompositeTypeProvider provider = new ReflectionCompositeTypeProvider(new ReflectionCompositeDelegateFactory());
            CompositeType compositeType;
            provider.TryGet(typeof(UserModel), out compositeType);
            sw.Stop();
            buildCompositeType = sw.ElapsedMilliseconds;


            // COMPOSITE FORMATTER.
            Converts.Repository
                .AddJsonEnumSearchHandler()
                .AddJsonPrimitivesSearchHandler()
                .AddJsonObjectSearchHandler();

            CompositeTypeFormatter formatter = new CompositeTypeFormatter(provider, new DefaultFactory<JsonCompositeStorage>());
            sw.Restart();
            for (int i = 0; i < count; i++)
            {
                UserModel model = new UserModel(1, "UserName", "Password");
                using (MemoryStream stream = new MemoryStream())
                {
                    Task<bool> isSerializedTask = formatter.TrySerializeAsync(model, new DefaultSerializerContext(typeof(UserModel), stream));
                    isSerializedTask.Wait();

                    stream.Seek(0, SeekOrigin.Begin);

                    string json = null;
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8, true, 1024, true))
                        json = reader.ReadToEnd();

                    stream.Seek(0, SeekOrigin.Begin);

                    IDeserializerContext context = new DefaultDeserializerContext(typeof(UserModel));
                    Task<bool> isDeserializedTask = formatter.TryDeserializeAsync(stream, context);
                    isDeserializedTask.Wait();

                    model = (UserModel)context.Output;
                }
            }
            sw.Stop();
            composite = sw.ElapsedMilliseconds;



            // COMPOSITE STORAGE
            sw.Restart();
            for (int i = 0; i < count; i++)
            {
                ICompositeStorage storage = new JsonCompositeStorage();
                storage.Add("Name", "Test.UserModel");
                storage.Add("Version", 1);

                ICompositeStorage payloadStorage = storage.Add("Payload");
                payloadStorage.Add("FirstName", "John");
                payloadStorage.Add("LastName", "Doe");
                payloadStorage.Add("Direction", ListSortDirection.Descending);
                payloadStorage.Add("IDs", new int[] { 1, 2, 3 });
                payloadStorage.Add("Keys", new List<int>() { 4, 5, 6 });

                using (MemoryStream stream = new MemoryStream())
                {
                    storage.StoreAsync(stream).Wait();
                    stream.Seek(0, SeekOrigin.Begin);

                    string json = null;
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8, true, 1024, true))
                        json = reader.ReadToEnd();

                    stream.Seek(0, SeekOrigin.Begin);
                    storage.LoadAsync(stream).Wait();
                }

                string value;
                storage.TryGet("Name", out value);
                int version;
                storage.TryGet("Version", out version);
                storage.TryGet("Payload", out payloadStorage);
                payloadStorage.TryGet("FirstName", out value);
                payloadStorage.TryGet("LastName", out value);

                ListSortDirection direction;
                Assert.AreEqual(true, payloadStorage.TryGet("Direction", out direction));
                int[] ids;
                payloadStorage.TryGet("IDs", out ids);
                List<int> keys;
                payloadStorage.TryGet("Keys", out keys);
            }
            sw.Stop();
            compositeStorage = sw.ElapsedMilliseconds;


            if (Directory.Exists("C:/Temp"))
            {
                File.WriteAllLines("C:/Temp/FormatterPerf.txt", new string[] 
                {
                    String.Format("Newtonsoft.Json directly:    {0}ms", newtonSoft),
                    String.Format("Build composite type:        {0}ms", buildCompositeType),
                    String.Format("Composite+Newtonsoft.Json:   {0}ms", composite),
                    String.Format("CompositeStorage:   {0}ms", compositeStorage)
                });
            }
        }
    }
}
