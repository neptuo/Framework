using Neptuo.Jobs.Handlers.Behaviors;
using Neptuo.Jobs.Handlers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestConsole.Services.Jobs
{
    class TempCheckServiceHandler : ServiceHandlerBase
    {
        private Timer timer;

        public TempCheckServiceHandler()
        {
            timer = new Timer(OnTime);
        }

        private void OnTime(object state)
        {
            Console.WriteLine(
                "Last write to 'C:/Temp': {0}; ThreadID: {1}",
                Directory.GetLastWriteTime(@"C:\Temp"),
                Thread.CurrentThread.ManagedThreadId
            );
        }

        protected override void OnStart()
        {
            timer.Change(TimeSpan.Zero, TimeSpan.FromSeconds(5));
        }

        protected override void OnStop()
        {
            timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            timer.Dispose();
        }
    }

    class Temp2CheckServiceHandler : ThreadServiceHandler
    {
        protected override void OnInvoke(WaitHandle shutdownHandle)
        {
            while (true)
            {
                if (shutdownHandle.WaitOne(TimeSpan.FromSeconds(2)))
                    break;

                Console.WriteLine(
                    "Last write to 'C:/Temp': {0}; ThreadID: {1}", 
                    Directory.GetLastWriteTime(@"C:\Temp"), 
                    Thread.CurrentThread.ManagedThreadId
                );
            }
        }
    }

    [Reprocess]
    public class TempCheckWorkerHandler : IBackgroundHandler
    {
        bool isFirst = true;

        public void Invoke()
        {
            if (isFirst)
            {
                isFirst = false;
                Console.WriteLine("Not supported...");
                throw new NotSupportedException();
            }

            Console.WriteLine(
                "Last write to 'C:/Temp': {0}; ThreadID: {1}",
                Directory.GetLastWriteTime(@"C:\Temp"),
                Thread.CurrentThread.ManagedThreadId
            );
        }
    }


}
