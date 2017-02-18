using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Exceptions.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Exceptions
{
    [TestClass]
    public class T_Exceptions_TryCatch
    {
        private readonly AnyHandler handler;
        private readonly TryCatch tryCatch;

        public T_Exceptions_TryCatch()
        {
            handler = new AnyHandler();
            tryCatch = new TryCatch(new ExceptionHandlerBuilder().Handler(handler));
        }

        [TestMethod]
        public void RunAction()
        {
            handler.Count = 0;

            bool state = tryCatch.Run(() =>
            {
                throw new InvalidOperationException();
            });
            Assert.AreEqual(false, state);
            Assert.AreEqual(1, handler.Count);

            state = tryCatch.Run(() => Console.WriteLine("Hello, World!"));
            Assert.AreEqual(true, state);
            Assert.AreEqual(1, handler.Count);
        }

        [TestMethod]
        public void RunFunc()
        {
            handler.Count = 0;

            string result;
            bool state = tryCatch.Run(() => "Hello, World!", out result);
            Assert.AreEqual(true, state);
            Assert.AreEqual("Hello, World!", result);
            Assert.AreEqual(0, handler.Count);

            state = tryCatch.Run(() =>
            {
                throw new InvalidOperationException();
            }, out result);
            Assert.AreEqual(false, state);
            Assert.AreEqual(default(string), result);
            Assert.AreEqual(1, handler.Count);
        }

        [TestMethod]
        public void RunFuncWithReturnValue()
        {
            handler.Count = 0;

            string result = tryCatch.Run(() => "Hello, World!");
            Assert.AreEqual("Hello, World!", result);
            Assert.AreEqual(0, handler.Count);

            result = tryCatch.Run(ThrowException);
            Assert.AreEqual(default(string), result);
            Assert.AreEqual(1, handler.Count);
        }

        private string ThrowException()
        {
            throw new InvalidOperationException();
        }
    }
}
