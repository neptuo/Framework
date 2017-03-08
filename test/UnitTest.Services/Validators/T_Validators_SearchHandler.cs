using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Validators
{
    [TestClass]
    public class T_Validators_SearchHandler
    {
        [TestMethod]
        public void Found()
        {
            DefaultValidationDispatcher dispatcher = new DefaultValidationDispatcher();
            dispatcher.AddSearchHandler(TryGetValidationHandler);

            Assert.AreEqual(true, dispatcher.Validate("Hello, World!").IsValid);
            Assert.AreEqual(false, dispatcher.Validate("Xyz").IsValid);
        }

        [TestMethod]
        [ExpectedException(typeof(MissingValidationHandlerException))]
        public void NotFound()
        {
            DefaultValidationDispatcher dispatcher = new DefaultValidationDispatcher();
            dispatcher.AddSearchHandler(TryGetValidationHandler);

            dispatcher.Validate(55);
        }

        private bool TryGetValidationHandler(Type modelType, out object validationHandler)
        {
            if (modelType == typeof(string))
                validationHandler = new StringValidationHandler();
            else
                validationHandler = null;

            return validationHandler != null;
        }
    }
}
