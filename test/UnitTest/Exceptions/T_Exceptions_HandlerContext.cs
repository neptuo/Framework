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
    public class T_Exceptions_HandlerContext
    {
        [TestMethod]
        public void Builder()
        {
            ContextExceptionHandler<Exception> handler1 = new ContextExceptionHandler<Exception>(false);
            ContextExceptionHandler<Exception> handler2 = new ContextExceptionHandler<Exception>(true);
            ContextExceptionHandler<Exception> handler3 = new ContextExceptionHandler<Exception>(false);

            IExceptionHandler handler = new ExceptionHandlerBuilder()
                .Handler(handler1)
                .Handler(handler2)
                .Handler(handler3);

            handler.Handle(new Exception());

            Assert.AreEqual(1, handler1.Count);
            Assert.AreEqual(1, handler2.Count);
            Assert.AreEqual(0, handler3.Count);
        }

        [TestMethod]
        public void BuilderWithFilter()
        {
            ContextExceptionHandler<Exception> handler1 = new ContextExceptionHandler<Exception>(false);
            ContextExceptionHandler<Exception> handler2 = new ContextExceptionHandler<Exception>(false);
            ContextExceptionHandler<Exception> handler3 = new ContextExceptionHandler<Exception>(false);
            ContextExceptionHandler<Exception> handler4 = new ContextExceptionHandler<Exception>(false);
            ContextExceptionHandler<ArgumentException> handler5 = new ContextExceptionHandler<ArgumentException>(true);
            ContextExceptionHandler<ArgumentException> handler6 = new ContextExceptionHandler<ArgumentException>(false);

            ExceptionHandlerBuilder builder = new ExceptionHandlerBuilder();

            builder
                .Handler(handler1)
                .Handler(handler2)
                .Filter<Exception>()
                .Handler(handler3)
                .Handler(handler4)
                .Filter<ArgumentException>()
                .Handler(handler5)
                .Handler(handler6);

            IExceptionHandler handler = builder;

            handler.Handle(new Exception());
            handler.Handle(new ArgumentException());

            Assert.AreEqual(2, handler1.Count);
            Assert.AreEqual(2, handler2.Count);
            Assert.AreEqual(2, handler3.Count);
            Assert.AreEqual(2, handler4.Count);
            Assert.AreEqual(1, handler5.Count);
            Assert.AreEqual(0, handler6.Count);
        }
    }
}
