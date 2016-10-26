using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Converters
{
    [TestClass]
    public class T_Converters_Converter
    {
        [TestMethod]
        public void ToString()
        {
            Converts.Repository
                .AddToString<DateTime>("yyy-MM-dd")
                .AddToStringSearchHandler();

            Func<DateTime, string> converter = Converts.Repository.GetConverter<DateTime, string>();
            Assert.AreEqual("2015-09-01", converter(new DateTime(2015, 9, 1)));
        }

        [TestMethod]
        public void ParseInt()
        {
            OutFunc<string, int, bool> tryConverter = Converts.Repository.GetTryConverter<string, int>();

            int value;
            Assert.AreEqual(false, tryConverter("x", out value));

            tryConverter = Converts.Repository
                .AddStringTo<int>(Int32.TryParse, true)
                .GetTryConverter<string, int>();

            Assert.AreEqual(false, tryConverter("x", out value));
            Assert.AreEqual(true, tryConverter("15", out value));
            Assert.AreEqual(15, value);
        }
    }
}
