using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neptuo.AppServices.Handlers
{
    /// <summary>
    /// Periodical service handler based on timer.
    /// To execute periodical action, override method <see cref="TimerServiceHandler.OnInvoke"/>.
    /// </summary>
    public abstract class TimerServiceHandler : ServiceHandlerBase
    {
        private readonly Timer timer;
        private readonly TimeSpan startDelay;
        private readonly TimeSpan interval;

        /// <summary>
        /// Creates new instance, that first runs immediately after service start and than every <paramref name="interval"/>.
        /// </summary>
        /// <param name="interval">Amount of time between executions.</param>
        public TimerServiceHandler(TimeSpan interval)
            : this(TimeSpan.Zero, interval)
        { }

        /// <summary>
        /// Creates new instance, that first runs <paramref name="startDelay"/> after service start and than every <paramref name="interval"/>.
        /// </summary>
        /// <param name="startDelay">Amount of time to first execute after service start.</param>
        /// <param name="interval">Amount of time between executions.</param>
        public TimerServiceHandler(TimeSpan startDelay, TimeSpan interval)
        {
            timer = new Timer(OnInvokeInternal, null, Timeout.Infinite, Timeout.Infinite);
            this.startDelay = startDelay;
            this.interval = interval;
        }

        /// <summary>
        /// Method invoked in defined period.
        /// </summary>
        protected abstract void OnInvoke();

        private void OnInvokeInternal(object state)
        {
            OnInvoke();
        }

        protected override void OnStart()
        {
            timer.Change(startDelay, interval);
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
