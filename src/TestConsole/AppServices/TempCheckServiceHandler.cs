using Neptuo.AppServices.Handlers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestConsole.AppServices
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
            Console.WriteLine("Last write to C:/Temp {0}", Directory.GetLastWriteTime(@"C:\Temp"));
        }

        protected override void OnStart()
        {
            timer.Change(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(5));
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
}
