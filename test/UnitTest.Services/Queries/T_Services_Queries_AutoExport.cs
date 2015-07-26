using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Activators;
using Neptuo.Reflections;
using Neptuo.Services.Queries.Handlers;
using Neptuo.Services.Queries.Handlers.AutoExports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Queries
{
    [TestClass]
    public class T_Services_Queries_AutoExport
    {
        [TestClass]
        public class Dependency
        {
            [TestMethod]
            public void Base()
            {
                IDependencyContainer root = new SimpleDependencyContainer();

                IReflectionService reflectionService = ReflectionFactory.FromCurrentAppDomain();
                using (ITypeExecutorService executorService = reflectionService.PrepareTypeExecutors())
                {
                    executorService.AddQueryHandlers(root);
                }

                IQueryHandler<Q1, R1> handler1 = root.Resolve<IQueryHandler<Q1, R1>>();
                IQueryHandler<Q2, R2> handler2 = root.Resolve<IQueryHandler<Q2, R2>>();
            }

            [TestMethod]
            public void ConcreteType()
            {
                IDependencyContainer root = new SimpleDependencyContainer();

                IReflectionService reflectionService = ReflectionFactory.FromCurrentAppDomain();
                using (ITypeExecutorService executorService = reflectionService.PrepareTypeExecutors())
                {
                    executorService.AddQueryHandlers(root);
                }

                IQueryHandler<Q3, R3> handler1 = root.Resolve<IQueryHandler<Q3, R3>>();

                try
                {
                    IQueryHandler<Q4, R4> handler2 = root.Resolve<IQueryHandler<Q4, R4>>();
                    Assert.Fail("Handler for Q4 should not be registered");
                }
                catch (DependencyRegistrationFailedException)
                { }
            }
        }

        [TestClass]
        public class Collection
        {
            [TestMethod]
            public void Base()
            {
                IQueryHandlerCollection collection = new DefaultQueryDispatcher();

                IReflectionService reflectionService = ReflectionFactory.FromCurrentAppDomain();
                using (ITypeExecutorService executorService = reflectionService.PrepareTypeExecutors())
                {
                    executorService.AddQueryHandlers(collection);
                }

                IQueryHandler<Q1, R1> handler1;
                Assert.AreEqual(true, collection.TryGet(out handler1));
                IQueryHandler<Q2, R2> handler2;
                Assert.AreEqual(true, collection.TryGet(out handler2));
            }

            [TestMethod]
            public void ConcreteType()
            {
                IQueryHandlerCollection collection = new DefaultQueryDispatcher();

                IReflectionService reflectionService = ReflectionFactory.FromCurrentAppDomain();
                using (ITypeExecutorService executorService = reflectionService.PrepareTypeExecutors())
                {
                    executorService.AddQueryHandlers(collection);
                }

                IQueryHandler<Q3, R3> handler3;
                Assert.AreEqual(true, collection.TryGet(out handler3));
                IQueryHandler<Q4, R4> handler4;
                Assert.AreEqual(false, collection.TryGet(out handler4));
            }
        }
    }
}
