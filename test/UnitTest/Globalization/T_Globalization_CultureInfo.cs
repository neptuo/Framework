using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Globalization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Globalization
{
    [TestClass]
    public class T_Globalization_CultureInfo
    {
        [TestMethod]
        public void Parents()
        {
            CultureInfo cultureInfo = new CultureInfo("cs-CZ");
            IEnumerable<CultureInfo> parents = cultureInfo.Parents();
            Assert.AreEqual(1, parents.Count());
        }

        [TestMethod]
        public void ParentsWithSelf()
        {
            CultureInfo cultureInfo = new CultureInfo("cs-CZ");
            IEnumerable<CultureInfo> parents = cultureInfo.ParentsWithSelf();
            Assert.AreEqual(2, parents.Count());
        }

        [TestMethod]
        public void Parser()
        {
            CultureInfo cultureInfo;
            Assert.AreEqual(true, CultureInfoParser.TryParse("cs-cz", out cultureInfo));
            Assert.AreEqual(true, CultureInfoParser.TryParse("cs", out cultureInfo));
            Assert.AreEqual(true, CultureInfoParser.TryParse("en", out cultureInfo));
            Assert.AreEqual(true, CultureInfoParser.TryParse("en-US", out cultureInfo));
            Assert.AreEqual(false, CultureInfoParser.TryParse("xx-XX", out cultureInfo));
            Assert.AreEqual(false, CultureInfoParser.TryParse("123", out cultureInfo));
            Assert.AreEqual(false, CultureInfoParser.TryParse(null, out cultureInfo));
        }
    }
}
