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
            Converts.RepositoryBuilder.UseExceptionSink();
            Converts.Repository.Add<string, int>(Int32.Parse);
            Assert.AreEqual(false, Converts.Repository.TryConvert("x", out int i));
        }

        [TestMethod]
        public void ExceptionHandlingConverter()
        {
            Converts.Repository.Add(
                new ExceptionHandlingConverter<string, int>(
                    new DefaultConverter<string, int>((string input, out int output) =>
                    {
                        output = Int32.Parse(input);
                        return true;
                    }),
                    e => { }
                )
            );
            Assert.AreEqual(false, Converts.Repository.TryConvert("x", out int i));
        }
    }
}
