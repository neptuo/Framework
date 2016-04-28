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

            builder
                .Handler(new ArgumentExceptionHandler());

            builder
                .Filter<AggregateRootException>()
                .Filter(a => a.InnerException == null)
                .Handler(new AggregateRootExceptionHandler());

            IExceptionHandlerCollection collection = new DefaultExceptionHandlerCollection()
                .Add(builder);
        }
    }
}
