using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Events.Handlers;
using Neptuo.Exceptions;
using Neptuo.Internals;
using Neptuo.Linq.Expressions;
using Orders.Domains.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.EventSourcing
{
    [TestClass]
    public class T_EventSourcing_PersistentEventDispatcher
    {
        [TestMethod]
        public void AddHandler()
        {
            var descriptorProvider = new HandlerDescriptorProvider(
                typeof(IEventHandler<>),
                typeof(IEventHandlerContext<>),
                TypeHelper.MethodName<IEventHandler<object>, object, Task>(h => h.HandleAsync),
                new DefaultExceptionHandlerCollection(),
                new DefaultExceptionHandlerCollection()
            );

            HandlerDescriptor descriptor = descriptorProvider.Get(new OrderPlacedHandler(), typeof(OrderPlaced));
            Assert.AreEqual(true, descriptor.IsPlain);
            Assert.AreEqual(false, descriptor.IsEnvelope);
            Assert.AreEqual(false, descriptor.IsContext);
        }
    }
}
