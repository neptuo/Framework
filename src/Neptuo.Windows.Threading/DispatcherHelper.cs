using Neptuo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Neptuo.Windows.Threading
{
    /// <summary>
    /// Encapsulates usage of Dispatcher.
    /// </summary>
    public class DispatcherHelper
    {
        private const int intervalCheckDelay = 1000;

        private DispatcherPriority priority;
        private Dispatcher dispatcher;

        /// <summary>
        /// Creates new instance of helper.
        /// </summary>
        /// <param name="dispatcher">Dispatcher to use.</param>
        /// <param name="priority">Priority of operations.</param>
        public DispatcherHelper(Dispatcher dispatcher, DispatcherPriority priority = DispatcherPriority.Normal)
        {
            Ensure.NotNull(dispatcher, "dispatcher");
            this.dispatcher = dispatcher;
            this.priority = priority;
        }

        /// <summary>
        /// Runs <paramref name="action"/>, eventually after <paramref name="delay"/>.
        /// Parameter <paramref name="priority"/> overrides priority from ctor.
        /// </summary>
        /// <param name="action">Action to run.</param>
        /// <param name="delay">Delay, before executing <paramref name="action"/>.</param>
        /// <param name="priority">Priority override.</param>
        public void Run(Action action, int delay = 0, DispatcherPriority priority = DispatcherPriority.Normal)
            => Run(dispatcher, action, delay, priority);

        /// <summary>
        /// Runs <paramref name="action"/> on <paramref name="dispatcher"/>, eventually after <paramref name="delay"/>.
        /// Parameter <paramref name="priority"/> sets action priority.
        /// </summary>
        /// <param name="dispatcher">Dispatcher to use.</param>
        /// <param name="action">Action to run.</param>
        /// <param name="delay">Delay, before executing <paramref name="action"/>.</param>
        /// <param name="priority">Opeartion priority.</param>
        public static void Run(Dispatcher dispatcher, Action action, int delay = 0, DispatcherPriority priority = DispatcherPriority.Normal)
        {
            Ensure.NotNull(dispatcher, "dispatcher");
            Ensure.NotNull(action, "action");

            ThreadStart start = delegate()
            {
                if (delay >= 0)
                    Thread.Sleep(delay);

                DispatcherOperation op = dispatcher.BeginInvoke(priority, action);
                DispatcherOperationStatus status = op.Status;
                while (status != DispatcherOperationStatus.Completed)
                    status = op.Wait(TimeSpan.FromMilliseconds(intervalCheckDelay));
            };
            new Thread(start).Start();
        }
    }
}
