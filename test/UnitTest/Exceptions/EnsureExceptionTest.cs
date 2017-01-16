using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Exceptions
{
    [TestClass]
    public class EnsureExceptionTest
    {
        public enum Value
        {
            One,
            Two
        }

        [TestMethod]
        public void NotSupportedEnumValue()
        {
            NotSupportedException exception = Ensure.Exception.NotSupported(Value.Two);
            Assert.IsNotNull(exception);
            Assert.AreEqual($"The value '{nameof(Value.Two)}' from the '{typeof(Value).FullName}' is not supported in this context.", exception.Message);
        }
    }
}
