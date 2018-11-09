using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Converters
{
    [TestClass]
    public class T_Converters_ConverterRepositoryBuilder : TestConvertersBase
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void UseBuilderAfterRepositoryIsCreated()
        {
            Converts.Repository.Add<string, int>(Int32.TryParse);
            Converts.RepositoryBuilder.UseExceptionSink();
        }
    }
}
