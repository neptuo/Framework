using Neptuo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neptuo.Windows.Threading
{
    /// <summary>
    /// A helper for throttling requests to execute action.
    /// Subsequent requests within a delay are kill and only the last one is executed.
    /// </summary>
    public class ThrottlingHelper
    {
        private readonly DispatcherHelper dispatcher;
        private readonly Action action;
        private readonly int delay;
        private int index = 0;

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="dispatcher">A threading helper.</param>
        /// <param name="action">An action to execute.</param>
        /// <param name="delay">A delay to kill requests between.</param>
        public ThrottlingHelper(DispatcherHelper dispatcher, Action action, int delay = 100)
        {
            Ensure.NotNull(dispatcher, "dispatcher");
            Ensure.NotNull(action, "action");
            Ensure.PositiveOrZero(delay, "delay");
            this.dispatcher = dispatcher;
            this.action = action;
            this.delay = delay;
        }

        /// <summary>
        /// Tries to run the action within the delay.
        /// </summary>
        public void Run()
        {
            Task.Factory.StartNew(
                index =>
                {
                    Thread.Sleep(delay);

                    if ((int)index == this.index)
                        dispatcher.Run(action);

                },
                ++this.index
            );
        }
    }
}
