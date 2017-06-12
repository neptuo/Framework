using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Converters
{
    [TestClass]
    public class T_Converters_ExceptionHandling
    {
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void DefaultHandler()
        {
            Converts.Repository.Add<string, int>(Int32.Parse);
            Converts.Repository.TryConvert("x", out int i);
        }

        [TestMethod]
        public void Sink()
        {
            DefaultConverterRepository repository = new DefaultConverterRepository(e => { });
            repository.Add<string, int>(Int32.Parse);
            Assert.AreEqual(false, repository.TryConvert("x", out int i));
        }
    }
}
