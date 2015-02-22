using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestConsole.Timers
{
    class TestTimers
    {
        public static void Test()
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
