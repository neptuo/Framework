using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Formatters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Formatters.Composite.Json
{
    [TestClass]
    public class UseJsonStorage
    {
        [TestMethod]
        public void Base()
        {
            ICompositeStorage storage = new JsonCompositeStorage();
            storage.Add("Name", "Test.UserModel");
            storage.Add("Version", 1);

            ICompositeStorage payloadStorage = storage.Add("Payload");
            payloadStorage.Add("FirstName", "John");
            payloadStorage.Add("LastName", "Doe");

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

            object value;

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
        }
    }
}
