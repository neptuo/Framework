using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Activators.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators
{
    public class T_Activators_Simple
    {
        [TestClass]
        public class Registration : TestRegistration
        {
            protected override IDependencyContainer CreateContainer()
            {
                return new SimpleDependencyContainer();
            }

            [TestMethod]
            [ExpectedException(typeof(DependencyRegistrationFailedException))]
            public void MappingToAbstractClass()
            {
                IDependencyContainer root = CreateContainer();
                root.Definitions
                    .AddTransient<IOutputWriter, OutputWriterBase>();

                IOutputWriter writer = root.Resolve<IOutputWriter>();
            }
        }

        [TestClass]
        public class Resolve : TestResolve
        {
            protected override IDependencyContainer CreateContainer()
            {
                return new SimpleDependencyContainer();
            }

            [TestMethod]
            public void DependencyProperties()
            {
                IDependencyContainer root = CreateContainer();
                root.Definitions
                    .AddTransient<IMessageFormatter, StringMessageFormatter>()
                    .AddTransient<IHelloService, HiService>()
                    .AddTransient<IOutputWriter, ConsoleOutputWriter>()
                    .AddTransient<View>();

                View view = root.Resolve<View>();
                Assert.IsNotNull(view.Writer);
                Assert.IsInstanceOfType(view.Writer, typeof(ConsoleOutputWriter));
                Assert.IsNotNull(view.HelloService);
                Assert.IsInstanceOfType(view.HelloService, typeof(HiService));
                Assert.IsNull(view.MessageFormatter);
            }
        }
    }
}
