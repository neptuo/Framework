using Neptuo.Events;
using Neptuo.Events.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestConsole.Events
{
    class TestEventManager : TestClass
    {
        public static void Test()
        {
            DefaultEventManager eventManager = new DefaultEventManager();
            eventManager.Add(DelegateEventHandler.FromAction<EventData>(e => Console.WriteLine("{0}: ThreadID: {1}", e.Index, Thread.CurrentThread.ManagedThreadId)));

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Before run {0};", i);
                eventManager.PublishAsync(new EventData(i));
                Console.WriteLine("After run {0};", i);
            }

            
        }
    }
}
