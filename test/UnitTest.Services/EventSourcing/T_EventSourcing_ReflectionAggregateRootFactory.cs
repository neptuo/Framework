using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Activators;
using Neptuo.Commands;
using Neptuo.Data;
using Neptuo.Data.Entity;
using Neptuo.Events;
using Neptuo.Formatters;
using Neptuo.Models.Domains;
using Neptuo.Models.Keys;
using Neptuo.Models.Repositories;
using Orders;
using Orders.Commands;
using Orders.Commands.Handlers;
using Orders.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.EventSourcing
{
    [TestClass]
    public class T_EventSourcing_ReflectionAggregateRootFactory
    {
        [TestMethod]
        public void WithoutParameters()
        {
            ReflectionAggregateRootFactory<AggregateWithParameters> factory = new ReflectionAggregateRootFactory<AggregateWithParameters>();
            AggregateWithParameters instance = factory.Create(KeyFactory.Create(typeof(AggregateWithParameters)), new List<IEvent>());
            Assert.IsNull(instance.Service);
        }

        [TestMethod]
        public void BuilderWithParameter()
        {
            HiHelloService service = new HiHelloService();

            ReflectionAggregateRootFactoryBuilder builder = new ReflectionAggregateRootFactoryBuilder()
                .AddKey()
                .AddHistory()
                .Add<IHelloService>(service);

            ReflectionAggregateRootFactory<AggregateWithParameters> factory = new ReflectionAggregateRootFactory<AggregateWithParameters>(builder);
            AggregateWithParameters instance = factory.Create(KeyFactory.Create(typeof(AggregateWithParameters)), new List<IEvent>());
            Assert.AreEqual(service, instance.Service);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void BuilderWithParameterOfDifferentType()
        {
            ReflectionAggregateRootFactoryBuilder builder = new ReflectionAggregateRootFactoryBuilder()
                .AddKey()
                .AddHistory()
                .Add("TEST");

            ReflectionAggregateRootFactory<AggregateWithParameters> factory = new ReflectionAggregateRootFactory<AggregateWithParameters>(builder);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void BuilderWithoutKey()
        {
            ReflectionAggregateRootFactoryBuilder builder = new ReflectionAggregateRootFactoryBuilder()
                .AddHistory()
                .Add<IHelloService>(new HiHelloService());

            ReflectionAggregateRootFactory<AggregateWithParameters> factory = new ReflectionAggregateRootFactory<AggregateWithParameters>(builder);
        }
    }
}
