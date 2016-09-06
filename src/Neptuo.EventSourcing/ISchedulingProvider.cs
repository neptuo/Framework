using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo
{
    /// <summary>
    /// The date and time provider for scheduling delayed commands and events.
    /// </summary>
    public interface ISchedulingProvider
    {
        /// <summary>
        /// Computes a delay after which command or event will be executed.
        /// </summary>
        /// <param name="executeAt">The date and time when execution should be done.</param>
        /// <returns>A delay after which command or event will be executed.</returns>
        [Obsolete]
        TimeSpan Compute(DateTime executeAt);

        /// <summary>
        /// When <c>true</c> is returned, the <paramref name="envelope"/> should be scheduled for later execution using <see cref="Schedule(ISchedulingContext)"/>.
        /// </summary>
        /// <param name="envelope">The envelope to be tested for later execution.</param>
        /// <returns>
        /// <c>true</c> is returned, the <paramref name="envelope"/> should be scheduled for later execution using <see cref="Schedule(ISchedulingContext)"/>.
        /// <c>false</c> when the <paramref name="envelope"/> should be executed immediately.
        /// </returns>
        bool IsLaterExecutionRequired(Envelope envelope);

        /// <summary>
        /// Schedules <paramref name="context"/> to be executed in the future.
        /// </summary>
        /// <param name="context">The context to be executed in the future.</param>
        void Schedule(ISchedulingContext context);
    }
}
