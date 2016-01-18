using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Formatters.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Formatters.Composite.Json
{
    [TestClass]
    public class T_Formatters_EnumConverter
    {
        [TestMethod]
        public void FromIntToJson()
        {
            JsonEnumConverter converter = new JsonEnumConverter(JsonEnumConverterType.UseInderlayingValue);

            object value;
            Assert.AreEqual(true, converter.TryConvert(typeof(ListSortDirection), typeof(JValue), ListSortDirection.Descending, out value));
            Assert.IsInstanceOfType(value, typeof(JValue));

            JValue jValue = (JValue)value;
            Assert.AreEqual(JTokenType.Integer, jValue.Type);
            Assert.AreEqual((long)ListSortDirection.Descending, jValue.Value);
        }

        [TestMethod]
        public void FromNullableIntToJson()
        {
            JsonEnumConverter converter = new JsonEnumConverter(JsonEnumConverterType.UseInderlayingValue);
            ListSortDirection? direction = ListSortDirection.Descending;

            object value;
            Assert.AreEqual(true, converter.TryConvert(typeof(ListSortDirection?), typeof(JValue), direction, out value));
            Assert.IsInstanceOfType(value, typeof(JValue));

            JValue jValue = (JValue)value;
            Assert.AreEqual(JTokenType.Integer, jValue.Type);
            Assert.AreEqual((long)ListSortDirection.Descending, jValue.Value);

            Assert.AreEqual(true, converter.TryConvert(typeof(ListSortDirection?), typeof(JValue), null, out value));
            Assert.AreEqual(null, value);
        }

        [TestMethod]
        public void FromJsonToInt()
        {
            JsonEnumConverter converter = new JsonEnumConverter(JsonEnumConverterType.UseInderlayingValue);

            object value;
            Assert.AreEqual(true, converter.TryConvert(typeof(JValue), typeof(ListSortDirection), new JValue((int)ListSortDirection.Descending), out value));
            Assert.AreEqual(ListSortDirection.Descending, value);
        }

        [TestMethod]
        public void FromJsonToNullableInt()
        {
            JsonEnumConverter converter = new JsonEnumConverter(JsonEnumConverterType.UseInderlayingValue);

            object value;
            Assert.AreEqual(true, converter.TryConvert(typeof(JValue), typeof(ListSortDirection?), new JValue((object)null), out value));
            Assert.AreEqual(null, value);
        }
    }
}
