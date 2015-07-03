using Microsoft.Practices.Unity;
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
			//TestUnityContainer();
        }
		
		static void TestUnityContainer() 
		{
			IUnityContainer container = new UnityContainer();
			container.RegisterType<IHelloService, HiService>();
			
			using (IUnityContainer child = container.CreateChildContainer())
			{
				child.RegisterType<IMessageWriter, TextMessageWriter>();
				PrintRegistrations(container);
                PrintRegistrations(child);
            }
            PrintRegistrations(container);
			
			container.Dispose();
		}
		
		static void PrintRegistrations(IUnityContainer container) 
		{
		    Console.WriteLine("Count: {0}", container.Registrations.Count());
			//foreach (var )
		}

        private static void TestUnity()
        {
            IDependencyContainer container = new UnityDependencyContainer()
                .AddTransient<IHelloService, HiService>()
                .AddNameScoped<IMessageWriter, ConsoleWriterActivator>("Request", new ConsoleWriterActivator())
                .AddTransient<Presenter>();



            IDependencyContainer container = new UnityDependencyContainer()
                .Map<IHelloService>().InAnyScope().ToType<HiService>()
                .Map<IMessageWriter>().InNamedScope("Request").ToActivator(new ConsoleWriterActivator())
                .Map<Presenter>().InTransient().ToSelf();

            using (IDependencyProvider provider = container.Scope("Request"))
            {
                Presenter presenter;

                using (IDependencyProvider provider2 = provider.Scope("Sub"))
                {
                    presenter = provider2.Resolve<Presenter>();
                    presenter.Execute();

                    presenter = provider2.Resolve<Presenter>();
                    presenter.Execute();
                }

                using (IDependencyProvider provider2 = provider.Scope("Sub"))
                {
                    presenter = provider2.Resolve<Presenter>();
                    presenter.Execute();
                }

                presenter = provider.Resolve<Presenter>();
                presenter.Execute();
            }

            using (IDependencyProvider provider = container.Scope("Request"))
            {
                Presenter presenter;

                using (IDependencyProvider provider2 = provider.Scope("Sub"))
                {
                    presenter = provider2.Resolve<Presenter>();
                    presenter.Execute();
                }

                using (IDependencyProvider provider2 = provider.Scope("Sub"))
                {
                    presenter = provider2.Resolve<Presenter>();
                    presenter.Execute();
                }

                presenter = provider.Resolve<Presenter>();
                presenter.Execute();
            }

            container.Dispose();
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