using Neptuo.AppServices.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neptuo.AppServices.Handlers
{
    /// <summary>
    /// Defines service running in its own thread.
    /// </summary>
    public abstract class ThreadServiceHandler : ServiceHandlerBase
    {
        private readonly ManualResetEvent shutDownEvent = new ManualResetEvent(false);
        private readonly TimeSpan stopTimeout;
        private Thread thread;

        public ThreadServiceHandler()
            : this(TimeSpan.FromMinutes(1))
        { }

        public ThreadServiceHandler(TimeSpan stopTimeout)
        {
            this.stopTimeout = stopTimeout;
        }

        /// <summary>
        /// Implementation should do it's work on custom thread.
        /// </summary>
        /// <param name="shutdownHandle"></param>
        protected abstract void OnExecute(WaitHandle shutdownHandle);

        protected override void OnStart()
        {
            thread = new Thread(OnStartThread);
            thread.Start();
        }

        private void OnStartThread()
        {
            OnExecute(shutDownEvent);
        }

        protected override void OnStop()
        {
            shutDownEvent.Set();
            try
            {
                if (!thread.Join(stopTimeout))
                    thread.Abort();
            }
            finally
            {
                thread = null;
                shutDownEvent.Reset();
            }
        }
    }
}
