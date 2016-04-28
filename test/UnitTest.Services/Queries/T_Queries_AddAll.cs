using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Queries.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Queries
{
    [TestClass]
    public class T_Services_Queries_AddAll
    {
        [TestMethod]
        public void Base()
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
    }
}
