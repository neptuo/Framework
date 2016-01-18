using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Formatters.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Formatters.Composite.Json
{
    [TestClass]
    public class T_Formatters_PrimitiveConverter
    {
        [TestMethod]
        public void ToJson()
        {
            JsonPrimitiveConverter converter = new JsonPrimitiveConverter();

            object value;
            Assert.AreEqual(true, converter.TryConvert(typeof(int), typeof(JValue), 1, out value));
            Assert.AreEqual(new JValue(1), value);

            Assert.AreEqual(true, converter.TryConvert(typeof(string), typeof(JValue), "Test", out value));
            Assert.AreEqual(new JValue("Test"), value);

            Assert.AreEqual(true, converter.TryConvert(typeof(TimeSpan), typeof(JValue), TimeSpan.FromSeconds(6620), out value));
            Assert.AreEqual(new JValue(TimeSpan.FromSeconds(6620)), value);
        }

        [TestMethod]
        public void FromJson()
        {
            JsonPrimitiveConverter converter = new JsonPrimitiveConverter();

            object value;
            Assert.AreEqual(true, converter.TryConvert(typeof(JValue), typeof(int), JValue.Parse("1"), out value));
            Assert.AreEqual(1, value);

            Assert.AreEqual(true, converter.TryConvert(typeof(JValue), typeof(string), new JValue("Test"), out value));
            Assert.AreEqual("Test", value);

            Assert.AreEqual(true, converter.TryConvert(typeof(JValue), typeof(TimeSpan), new JValue(TimeSpan.FromSeconds(6620)), out value));
            Assert.AreEqual(TimeSpan.FromSeconds(6620), value);
        }
    }
}
