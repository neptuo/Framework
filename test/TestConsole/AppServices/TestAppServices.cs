using Neptuo;
using Neptuo.AppServices;
using Neptuo.AppServices.Handlers;
using Neptuo.AppServices.Handlers.Behaviors;
using Neptuo.AppServices.Handlers.Behaviors.Hosting;
using Neptuo.AppServices.Handlers.Behaviors.Hosting.Compilation;
using Neptuo.AppServices.Handlers.Behaviors.Processing;
using Neptuo.AppServices.Handlers.Behaviors.Processing.Compilation;
using Neptuo.Compilers;
using Neptuo.ComponentModel.Behaviors;
using Neptuo.ComponentModel.Behaviors.Processing.Compilation;
using Neptuo.ComponentModel.Behaviors.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestConsole.AppServices
{
    class TestAppServices
    {
        public static void Test()
        {
            TestServices();
            //TestThreadingTimer();
        }

        private static void TestServices()
        {
            Console.WriteLine("Current ThreadID: {0}", Thread.CurrentThread.ManagedThreadId);

            IBehaviorCollection behaviors = new BehaviorProviderCollection()
                .Add(
                    new AttributeBehaviorProvider()
                        .AddMapping(typeof(ReprocessAttribute), typeof(ReprocessBehavior))
                );

            ICompilerConfiguration configuration = new CompilerConfiguration()
                .BaseType(typeof(WorkerPipelineHandler<>))
                .TempDirectory(@"C:\Temp\Pipelines");
            
            configuration.References()
                .AddDirectory(Environment.CurrentDirectory);

            configuration.BehaviorInstance()
                .AddGenerator(typeof(ReprocessAttribute), new CodeDomReprocessBehaviorInstanceGenerator());

            ServiceHandlerCollection collection = new ServiceHandlerCollection();
            //collection.Add(new TempCheckServiceHandler());
            collection.Add(
                new WorkerServiceCollection()
                    .AddIntervalHandler(TimeSpan.FromSeconds(5), new CodeDomWorkerPipelineHandler<TempCheckWorkerHandler>(new CodeDomWorkerPipelineConfiguration(behaviors, configuration)))
            );
            //collection.Add(new Temp2CheckServiceHandler());


            Console.WriteLine("Starting services...");
            collection.Start();
            Console.WriteLine("Press any key to stop services...");
            Console.ReadKey(true);
            collection.Stop();
            Console.WriteLine("All services stopped...");
        }

        private static void TestThreadingTimer()
        {
            Console.WriteLine("ThreadID: {0}", Thread.CurrentThread.ManagedThreadId);

            Timer timer = new Timer(OnTimer, 5, 0, 2000);

            Console.ReadKey(true);
            timer.Change(5000, 1000);

            Console.ReadKey(true);
            timer.Dispose();
        }

        private static void OnTimer(object state)
        {
            Console.WriteLine("State: {0}; DateTime: {1}; ThreadID: {2}", state, DateTime.Now, Thread.CurrentThread.ManagedThreadId);
        }
    }
}
