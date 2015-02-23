using Neptuo.AppServices;
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
            ServiceHandlerCollection collection = new ServiceHandlerCollection();
            collection.Add(new TempCheckServiceHandler());


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
