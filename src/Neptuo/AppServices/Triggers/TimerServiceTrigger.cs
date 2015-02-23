using Neptuo.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neptuo.AppServices.Triggers
{
    /// <summary>
    /// Implementation of <see cref="IServiceTrigger"/> for <see cref="System.Threading.Timer"/> and specified interval.
    /// </summary>
    public class TimerServiceTrigger : DisposableBase, IServiceTrigger
    {
        private readonly Timer timer;
        private readonly TimeSpan startDelay;
        private readonly TimeSpan interval;

        public event Action OnTrigger;

        /// <summary>
        /// Creates new instance which is triggered every <paramref name="interval"/>.
        /// </summary>
        /// <param name="interval">Amount of time between trigger hits.</param>
        public TimerServiceTrigger(TimeSpan interval)
            : this(TimeSpan.Zero, interval)
        { }
        
        /// <summary>
        /// Creates new instance, that is first hit <paramref name="startDelay"/> after service start and than every <paramref name="interval"/>.
        /// </summary>
        /// <param name="startDelay">Amount of time to first trigger hit after start.</param>
        /// <param name="interval">Amount of time between executions.</param>
        public TimerServiceTrigger(TimeSpan startDelay, TimeSpan interval)
        {
            timer = new Timer(OnTriggerInternal, null, Timeout.Infinite, Timeout.Infinite);
            this.startDelay = startDelay;
            this.interval = interval;
        }

        private void OnTriggerInternal(object state)
        {
            if (OnTrigger != null)
                OnTrigger();
        }

        public void Start()
        {
            timer.Change(TimeSpan.Zero, interval);
        }

        public void Stop()
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
