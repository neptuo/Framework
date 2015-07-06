﻿using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators
{
    public class TestUnityDependencyContainer
    {
        private static IDependencyContainer CreateContainer()
        {
            return new UnityDependencyContainer();
        }

        private static void TryCatchUnResolvable<T>(IDependencyProvider provider)
        {
            bool isResolved = false;
            try
            {
                T service = provider.Resolve<T>();
                isResolved = true;
            }
            catch (DependencyResolutionFailedException)
            { }

            if (isResolved)
                Assert.Fail("Should not be resolvable.");
        }

        [TestClass]
        public class DefinitionCollection
        {
            [TestMethod]
            public void Registration()
            {
                IDependencyContainer container = CreateContainer();
                container.Definitions
                    .AddNameScoped<IMessageFormatter, StringMessageFormatter>(container.ScopeName)
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

                container1.Definitions.AddTransient<IHelloService>();
                IDependencyDefinition definition1;
                Assert.IsTrue(container1.Definitions.TryGet(typeof(IHelloService), out definition1));
                Assert.IsTrue(definition1.IsResolvable);

                IDependencyContainer container2 = container1.Scope("S1");

                Assert.IsTrue(container2.Definitions.TryGet(typeof(IHelloService), out definition1));
                Assert.IsTrue(definition1.IsResolvable);

                container2.Definitions.AddTransient<IOutputWriter>();
                Assert.IsTrue(container2.Definitions.TryGet(typeof(IOutputWriter), out definition1));
                Assert.IsTrue(definition1.IsResolvable);

                Assert.IsFalse(container1.Definitions.TryGet(typeof(IOutputWriter), out definition1));
            }

            [TestMethod]
            public void RegisteredOnlyInsideScope()
            {
                IDependencyContainer root = CreateContainer();
                root.Definitions
                    .AddNameScoped<string>("S1", "Hello")
                    .AddNameScoped<int>("S2", 5);

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

        [TestClass]
        public class Target
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
        }
    }
}
