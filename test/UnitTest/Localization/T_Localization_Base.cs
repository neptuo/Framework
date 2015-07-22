using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Localization
{
    [TestClass]
    public class T_Localization_Base
    {
        [TestMethod]
        public void DefaultHandler()
        {
            string text = (L)"Hello, World!";
            Assert.AreEqual("Hello, World!", text);
        }
    }
}
