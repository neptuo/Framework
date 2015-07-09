using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators.Tests
{
    public abstract class TestResolve : TestBase
    {
        private void ResolveString(IDependencyProvider root)
        {
            using (IDependencyProvider s1 = root.Scope("S1"))
            {
                Assert.AreEqual("S1", s1.Resolve<string>());

                using (IDependencyProvider s2 = s1.Scope("S2"))
                {
                    Assert.AreEqual("S2", s2.Resolve<string>());
                }

                Assert.AreEqual("S1", s1.Resolve<string>());
            }
        }

        [TestMethod]
        public void BaseResolving()
        {
            IDependencyContainer root = CreateContainer();
            root.Definitions
                .AddNameScoped<string>("S1", "S1")
                .AddNameScoped<string>("S2", "S2");

            ResolveString(root);
        }

        [TestMethod]
        public void InstanceOfActivator()
        {
            IDependencyContainer root = CreateContainer();
            root.Definitions
                .AddNameScopedActivator<string, IActivator<string>>("S1", new InstanceActivator<string>(() => "S1"))
                .AddNameScopedActivator<string, IActivator<string>>("S2", new InstanceActivator<string>(() => "S2"));

            ResolveString(root);
        }

        [TestMethod]
        public void MappingTypeWithScopes()
        {
            IDependencyContainer root = CreateContainer();
            root.Definitions
                .AddNameScoped<IOutputWriter, StringOutputWriter>("S1")
                .AddNameScoped<IOutputWriter, ConsoleOutputWriter>("S2");

            IOutputWriter writer;
            using (IDependencyProvider s1 = root.Scope("S1"))
            {
                writer = s1.Resolve<IOutputWriter>();
                Assert.IsInstanceOfType(writer, typeof(StringOutputWriter));

                using (IDependencyProvider s2 = s1.Scope("S2"))
                {
                    writer = s2.Resolve<IOutputWriter>();
                    Assert.IsInstanceOfType(writer, typeof(ConsoleOutputWriter));
                }

                writer = s1.Resolve<IOutputWriter>();
                Assert.IsInstanceOfType(writer, typeof(StringOutputWriter));
            }

            using (IDependencyProvider s2 = root.Scope("S2"))
            {
                writer = s2.Resolve<IOutputWriter>();
                Assert.IsInstanceOfType(writer, typeof(ConsoleOutputWriter));
            }

            TryCatchUnResolvable<IOutputWriter>(root);
        }

        /// <summary>
        /// Tries to resolve registered type with dependency in scope.
        /// </summary>
        [TestMethod]
        public void DependencyInTextScope()
        {
            IDependencyContainer root = CreateContainer();
            root.Definitions
                .AddTransient<IHelloService, HiService>()
                .AddNameScoped<IMessageFormatter, StringMessageFormatter>("S1");

            IHelloService helloService;
            TryCatchUnResolvable<IHelloService>(root);

            using (IDependencyProvider s1 = root.Scope("S1"))
            {
                TryCatchUnResolvable<IHelloService>(root);

                helloService = s1.Resolve<IHelloService>();
                Assert.IsTrue(s1.TryResolve<IHelloService>(out helloService));
            }

            TryCatchUnResolvable<IHelloService>(root);
            Assert.IsFalse(root.TryResolve<IHelloService>(out helloService));
        }

        [TestMethod]
        public void InAnyScope()
        {
            IDependencyContainer root = CreateContainer();
            root.Definitions
                .AddScoped<Counter>();

            Counter counter;
            counter = root.Resolve<Counter>();
            Assert.AreEqual(1, counter.Count);

            counter = root.Resolve<Counter>();
            Assert.AreEqual(1, counter.Count);

            using (root.Scope("S1"))
            {
                counter = root.Resolve<Counter>();
                Assert.AreEqual(2, counter.Count);

                counter = root.Resolve<Counter>();
                Assert.AreEqual(2, counter.Count);

                using (root.Scope("S2"))
                {
                    counter = root.Resolve<Counter>();
                    Assert.AreEqual(3, counter.Count);

                    counter = root.Resolve<Counter>();
                    Assert.AreEqual(3, counter.Count);
                }

                counter = root.Resolve<Counter>();
                Assert.AreEqual(3, counter.Count);

                using (root.Scope("S2"))
                {
                    counter = root.Resolve<Counter>();
                    Assert.AreEqual(4, counter.Count);

                    counter = root.Resolve<Counter>();
                    Assert.AreEqual(4, counter.Count);
                }

                counter = root.Resolve<Counter>();
                Assert.AreEqual(4, counter.Count);
            }

            counter = root.Resolve<Counter>();
            Assert.AreEqual(4, counter.Count);

            using (root.Scope("S1"))
            {
                counter = root.Resolve<Counter>();
                Assert.AreEqual(5, counter.Count);

                counter = root.Resolve<Counter>();
                Assert.AreEqual(5, counter.Count);
            }

            counter = root.Resolve<Counter>();
            Assert.AreEqual(5, counter.Count);
        }
    }
}
