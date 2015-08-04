using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Converters
{
    [TestClass]
    public class T_Converter
    {
        [TestMethod]
        public void ToString()
        {
            string value;
            Assert.AreEqual(true, Converts.Try(5, out value));
            Assert.AreEqual("5", value);
        }
    }
}
