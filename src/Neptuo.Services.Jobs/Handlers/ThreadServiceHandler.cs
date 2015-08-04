using Neptuo.Services.Jobs.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neptuo.Services.Jobs.Handlers
{
    /// <summary>
    /// Defines service running in its own thread.
    /// </summary>
    public abstract class ThreadServiceHandler : ServiceHandlerBase
    {
        private readonly ManualResetEvent shutDownEvent = new ManualResetEvent(false);
        private readonly TimeSpan stopTimeout;
        private Thread thread;

        /// <summary>
        /// Creates new instance, which waits for one minute to shutdown its thread.
        /// </summary>
        public ThreadServiceHandler()
            : this(TimeSpan.FromMinutes(1))
        { }

        /// <summary>
        /// Creates new instance with <paramref name="stopTimeout"/> to wait for shutdown its thread.
        /// </summary>
        /// <param name="stopTimeout">Amount of time to wait for shutdown its thread.</param>
        public ThreadServiceHandler(TimeSpan stopTimeout)
        {
            this.stopTimeout = stopTimeout;
        }

        /// <summary>
        /// Implementation should do it's work on custom thread.
        /// </summary>
        /// <param name="shutdownHandle"></param>
        protected abstract void OnInvoke(WaitHandle shutdownHandle);

        private void OnInvokeInternal()
        {
            OnInvoke(shutDownEvent);
        }

        protected override void OnStart()
        {
            thread = new Thread(OnInvokeInternal);
            thread.Start();
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
