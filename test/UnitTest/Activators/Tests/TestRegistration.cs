using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators.Tests
{
    public abstract class TestRegistration : TestBase
    {
        [TestMethod]
        public void Registration()
        {
            IDependencyContainer container = CreateContainer();
            container.Definitions
                .AddScoped<IMessageFormatter, StringMessageFormatter>(container.ScopeName)
                .AddTransient<IHelloService, HiService>()
                .AddScoped<IOutputWriter, StringOutputWriter>()
                .AddTransient<Presenter>();

            IDependencyDefinition definition;
            Assert.AreEqual(container.Definitions.TryGet(typeof(IMessageFormatter), out definition), true);
        }

        [TestMethod]
        public void Basic()
        {
            IDependencyContainer container1 = CreateContainer();

            container1.Definitions.AddTransient<IHelloService, HiService>();
            IDependencyDefinition definition1;
            Assert.IsTrue(container1.Definitions.TryGet(typeof(IHelloService), out definition1));
            Assert.IsTrue(definition1.IsResolvable);

            IDependencyContainer container2 = container1.Scope("S1");

            Assert.IsTrue(container2.Definitions.TryGet(typeof(IHelloService), out definition1));
            Assert.IsTrue(definition1.IsResolvable);

            container2.Definitions.AddTransient<IOutputWriter, StringOutputWriter>();
            Assert.IsTrue(container2.Definitions.TryGet(typeof(IOutputWriter), out definition1));
            Assert.IsTrue(definition1.IsResolvable);

            Assert.IsFalse(container1.Definitions.TryGet(typeof(IOutputWriter), out definition1));
        }

        [TestMethod]
        public void RegisteredOnlyInsideScope()
        {
            IDependencyContainer root = CreateContainer();
            root.Definitions
                .AddScoped<string>("S1", "Hello")
                .AddScoped<int>("S2", 5);

            IDependencyDefinition definition;

            Assert.IsTrue(root.Definitions.TryGet(typeof(string), out definition));
            Assert.IsFalse(definition.IsResolvable);
            Assert.IsTrue(root.Definitions.TryGet(typeof(int), out definition));
            Assert.IsFalse(definition.IsResolvable);

            using (IDependencyContainer s1 = root.Scope("S1"))
            {
                Assert.IsTrue(s1.Definitions.TryGet(typeof(string), out definition));
                Assert.IsTrue(definition.IsResolvable);
                Assert.IsTrue(s1.Definitions.TryGet(typeof(int), out definition));
                Assert.IsFalse(definition.IsResolvable);

                using (IDependencyContainer s2 = s1.Scope("S2"))
                {
                    Assert.IsTrue(s2.Definitions.TryGet(typeof(string), out definition));
                    Assert.IsTrue(definition.IsResolvable);
                    Assert.IsTrue(s2.Definitions.TryGet(typeof(int), out definition));
                    Assert.IsTrue(definition.IsResolvable);
                }

                Assert.IsTrue(s1.Definitions.TryGet(typeof(string), out definition));
                Assert.IsTrue(definition.IsResolvable);
                Assert.IsTrue(s1.Definitions.TryGet(typeof(int), out definition));
                Assert.IsFalse(definition.IsResolvable);
            }

            Assert.IsTrue(root.Definitions.TryGet(typeof(string), out definition));
            Assert.IsFalse(definition.IsResolvable);
            Assert.IsTrue(root.Definitions.TryGet(typeof(int), out definition));
            Assert.IsFalse(definition.IsResolvable);
        }
    }
}
