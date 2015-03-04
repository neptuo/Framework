using Neptuo;
using Neptuo.Activators;
using Neptuo.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.DependencyContainers
{
    class TestDependency : TestClass
    {
        public static void Test()
        {
            TestUnity();
            //TestPerf();
        }

        private static void TestUnity()
        {
            IDependencyContainer container = new UnityDependencyContainer()
                .Add<IHelloService>().InTransient().ToType<HiService>()
                .Add<IMessageWriter>().InNamedScope("Root").ToType<ConsoleWriter>()
                .Add<Presenter>().InTransient().ToSelf();
        }

        static void TestPerf()
        {
            IDependencyProvider my = new SimpleDependencyProvider();
            IDependencyProvider unity = new UnityDependencyContainer();

            DebugIteration("My", 100000, () => my.Resolve<Service>());
            DebugIteration("unity", 100000, () => unity.Resolve<Service>());
        }
    }

    
}