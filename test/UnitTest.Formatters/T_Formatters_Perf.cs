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
using System.Text;
using System.Threading.Tasks;
using UnitTest.Formatters.Composite;

namespace UnitTest.Formatters
{
    [TestClass]
    public class T_Formatters_Perf
    {
        [TestMethod]
        public void SimplePerf()
        {
            int count = 100000;

            long rawNewtonSoft = RawNewtonsoftJson(count);
            long wrappedNewtonSoft = WrappedNewtonsoftJson(count);
            long buildCompositeType = BuildComposityType();
            long composite = CompositeTypeFormatter(count);
            long compositeStorage = CompositeStorage(count);
            //long wrappedXml = WrappedXml(count);

            if (Directory.Exists("C:/Temp"))
            {
                File.WriteAllLines("C:/Temp/FormatterPerf.txt", new string[] 
                {
                    String.Format("Newtonsoft.Json directly:    {0}ms", rawNewtonSoft),
                    String.Format("Newtonsoft.Json wrapped:     {0}ms", wrappedNewtonSoft),
                    String.Format("Build composite type:        {0}ms", buildCompositeType),
                    String.Format("Composite+Newtonsoft.Json:   {0}ms", composite),
                    String.Format("CompositeStorage:            {0}ms", compositeStorage)//,
                    //String.Format("XML wrapped:                 {0}ms", wrappedXml)
                });
            }
        }

        private long ReadWriteUsingFormatter(int count, ISerializer serializer, IDeserializer deserializer)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < count; i++)
            {
                UserModel model = new UserModel(1, "UserName", "Password");
                using (MemoryStream stream = new MemoryStream())
                {
                    Task<bool> isSerializedTask = serializer.TrySerializeAsync(model, new DefaultSerializerContext(typeof(UserModel), stream));
                    isSerializedTask.Wait();

                    stream.Seek(0, SeekOrigin.Begin);

                    string json = null;
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8, true, 1024, true))
                        json = reader.ReadToEnd();

                    stream.Seek(0, SeekOrigin.Begin);

                    IDeserializerContext context = new DefaultDeserializerContext(typeof(UserModel));
                    Task<bool> isDeserializedTask = deserializer.TryDeserializeAsync(stream, context);
                    isDeserializedTask.Wait();

                    model = (UserModel)context.Output;
                }
            }
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }

        private long RawNewtonsoftJson(int count)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < count; i++)
            {
                UserModel model = new UserModel(1, "UserName", "Password");
                string json = JsonConvert.SerializeObject(model);

                model = JsonConvert.DeserializeObject<UserModel>(json);
            }
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }

        private long WrappedNewtonsoftJson(int count)
        {
            JsonFormatter formatter = new JsonFormatter();
            return ReadWriteUsingFormatter(count, formatter, formatter);
        }

        private long WrappedXml(int count)
        {
            XmlFormatter formatter = new XmlFormatter();
            return ReadWriteUsingFormatter(count, formatter, formatter);
        }

        private long BuildComposityType()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            ICompositeTypeProvider provider = new ReflectionCompositeTypeProvider(new ReflectionCompositeDelegateFactory());
            CompositeType compositeType;
            provider.TryGet(typeof(UserModel), out compositeType);
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }

        private long CompositeTypeFormatter(int count)
        {
            Converts.Repository
                .AddJsonEnumSearchHandler()
                .AddJsonPrimitivesSearchHandler()
                .AddJsonObjectSearchHandler();

            ICompositeTypeProvider provider = new ReflectionCompositeTypeProvider(new ReflectionCompositeDelegateFactory());
            CompositeType compositeType;
            provider.TryGet(typeof(UserModel), out compositeType);

            CompositeTypeFormatter formatter = new CompositeTypeFormatter(provider, new DefaultFactory<JsonCompositeStorage>());
            Stopwatch sw = new Stopwatch();
            sw.Start();
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
            return sw.ElapsedMilliseconds;
        }

        private long CompositeStorage(int count)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
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
            return sw.ElapsedMilliseconds;
        }
    }
}
