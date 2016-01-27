using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo;
using Neptuo.Formatters;
using Neptuo.Formatters.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Formatters.Composite.Json
{
    [TestClass]
    public class T_Formatters_CompositeStorage
    {
        [TestMethod]
        public void Base()
        {
            Converts.Repository
                .AddJsonEnumSearchHandler()
                .AddJsonPrimitivesSearchHandler()
                .AddJsonObjectSearchHandler();

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
                storage.Store(stream);
                stream.Seek(0, SeekOrigin.Begin);

                string json = null;
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8, true, 1024, true))
                    json = reader.ReadToEnd();

                stream.Seek(0, SeekOrigin.Begin);
                storage.Load(stream);
            }

            string value;

            Assert.AreEqual(true, storage.TryGet("Name", out value));
            Assert.AreEqual("Test.UserModel", value);

            int version;
            Assert.AreEqual(true, storage.TryGet("Version", out version));
            Assert.AreEqual(1, version);

            Assert.AreEqual(true, storage.TryGet("Payload", out payloadStorage));

            Assert.AreEqual(true, payloadStorage.TryGet("FirstName", out value));
            Assert.AreEqual("John", value);

            Assert.AreEqual(true, payloadStorage.TryGet("LastName", out value));
            Assert.AreEqual("Doe", value);

            ListSortDirection direction;
            Assert.AreEqual(true, payloadStorage.TryGet("Direction", out direction));
            Assert.AreEqual(ListSortDirection.Descending, direction);

            int[] ids;
            Assert.AreEqual(true, payloadStorage.TryGet("IDs", out ids));
            Assert.IsNotNull(ids);
            Assert.AreEqual(1, ids[0]);
            Assert.AreEqual(2, ids[1]);
            Assert.AreEqual(3, ids[2]);

            List<int> keys;
            Assert.AreEqual(true, payloadStorage.TryGet("Keys", out keys));
            Assert.IsNotNull(ids);
            Assert.AreEqual(4, keys[0]);
            Assert.AreEqual(5, keys[1]);
            Assert.AreEqual(6, keys[2]);
        }
    }
}
