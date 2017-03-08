using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Validators
{
    [TestClass]
    public class T_Validators_MissingHandler
    {
        [TestMethod]
        [ExpectedException(typeof(MissingValidationHandlerException))]
        public void Exception()
        {
            DefaultValidationDispatcher dispatcher = new DefaultValidationDispatcher();
            dispatcher.Validate("Hello, World!");
        }

        [TestMethod]
        public void InValid()
        {
            DefaultValidationDispatcher dispatcher = new DefaultValidationDispatcher(false);
            IValidationResult result = dispatcher.Validate("Hello, World!");
            Assert.AreEqual(false, result.IsValid);
        }

        [TestMethod]
        public void Valid()
        {
            DefaultValidationDispatcher dispatcher = new DefaultValidationDispatcher(true);
            IValidationResult result = dispatcher.Validate("Hello, World!");
            Assert.AreEqual(true, result.IsValid);
        }
    }
}
