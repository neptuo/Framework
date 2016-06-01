using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Commands;
using Neptuo.Commands.Handlers;
using Neptuo.Queries;
using Neptuo.Queries.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo
{
    [TestClass]
    public class T_Services_AddAll
    {
        [TestMethod]
        public void Queries()
        {
            IQueryHandlerCollection collection = new DefaultQueryDispatcher();
            collection.AddAll(new Q1Handler());

            IQueryHandler<Q1, R1> handler1;
            Assert.AreEqual(true, collection.TryGet(out handler1));
            IQueryHandler<Q2, R2> handler2;
            Assert.AreEqual(true, collection.TryGet(out handler2));

            collection.AddAll(new Q2Handler());

            IQueryHandler<Q3, R3> handler3;
            Assert.AreEqual(true, collection.TryGet(out handler3));
            IQueryHandler<Q4, R4> handler4;
            Assert.AreEqual(true, collection.TryGet(out handler4));
        }

        [TestMethod]
        public void Commands()
        {
            ICommandHandlerCollection collection = new DefaultCommandDispatcher();
            collection.AddAll(new C1Handler());

            ICommandHandler<C1> handler1;
            Assert.AreEqual(true, collection.TryGet<C1>(out handler1));

            ICommandHandler<C2> handler2;
            Assert.AreEqual(true, collection.TryGet<C2>(out handler2));

            ICommandHandler<C3> handler3;
            Assert.AreEqual(true, collection.TryGet<C3>(out handler3));
        }
    }
}
