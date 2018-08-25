using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Exceptions.Handlers;
using Neptuo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Exceptions
{
    [TestClass]
    public class T_Services_Exceptions_Builder
    {
        [TestMethod]
        public void Base()
        {
            ExceptionHandlerBuilder builder = new ExceptionHandlerBuilder();

            ArgumentExceptionHandler handler1 = new ArgumentExceptionHandler();
            InnerExceptionIsNullHandler handler2 = new InnerExceptionIsNullHandler();
            MessageLongerThanTenHandler handler3 = new MessageLongerThanTenHandler();
            MessageLongerThanTenHandler handler4 = new MessageLongerThanTenHandler();

            builder
                .Handler(handler1);

            builder
                .Filter(a => a.InnerException == null)
                .Handler(handler2)
                .Filter(a => a.Message.Length > 10)
                .Handler(handler3);

            builder
                .Filter(a => a.Message.Length > 10)
                .Handler(handler4);

            IExceptionHandlerCollection collection = new DefaultExceptionHandlerCollection()
                .Add(builder);

            collection.Handle(new Exception("<= 10ch"));
            collection.Handle(new Exception("Long message > 10ch", new Exception()));
            collection.Handle(new Exception("===== 10ch"));

            collection.Handle(new ArgumentException("<= 10ch"));
            collection.Handle(new ArgumentException("Long message > 10ch"));
            collection.Handle(new ArgumentException("===== 10ch", new Exception()));

            Assert.AreEqual(3, handler1.CallCount);
            Assert.AreEqual(4, handler2.CallCount);
            Assert.AreEqual(1, handler3.CallCount);
            Assert.AreEqual(2, handler4.CallCount);
        }
    }
}
