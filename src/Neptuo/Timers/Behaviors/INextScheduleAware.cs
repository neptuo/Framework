using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Timers.Behaviors
{
    /// <summary>
    /// Defines service, that is aware of its next schedule.
    /// </summary>
    public interface INextScheduleAware
    {
        /// <summary>
        /// Should determine if service should be scheduled for next execution and provide next schedule date time.
        /// </summary>
        /// <param name="now">Current date time.</param>
        /// <param name="nextSchedule">Date time of next schedule.</param>
        /// <returns><c>true</c> to say 'Yes, schedule me for next execution'; <c>false</c> to say 'I'm finished', next schedule is not needed.</returns>
        bool NextSchedule(DateTime now, out DateTime nextSchedule);
    }
}
