﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Commands;
using Neptuo.Commands.Handlers;
using Neptuo.Events;
using Neptuo.Events.Handlers;
using Neptuo.Exceptions;
using Neptuo.Models;
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

        [TestMethod]
        public void Events()
        {
            DefaultEventManager manager = new DefaultEventManager();

            E1Handler handler = new E1Handler();
            manager.AddAll(handler);

            manager.PublishAsync(new E1()).Wait();
            Assert.AreEqual(1, handler.E1Count);
            manager.PublishAsync(new E2()).Wait();
            Assert.AreEqual(1, handler.E2Count);
            manager.PublishAsync(new E3()).Wait();
            Assert.AreEqual(1, handler.E3Count);
        }

        [TestMethod]
        public void Exceptions()
        {
            DefaultExceptionHandlerCollection collection = new DefaultExceptionHandlerCollection();
            MultiHandler handler = new MultiHandler();
            collection.AddAll(handler);

            collection.Handle(new ArgumentException());
            collection.Handle(new ArgumentException());
            collection.Handle(new AggregateRootException());

            Assert.AreEqual(2, handler.ArgumentCount);
            Assert.AreEqual(1, handler.AggregateRootCount);
        }
    }
}
