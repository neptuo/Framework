using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo
{
    /// <summary>
    /// The implementation of <see cref="ISchedulingProvider"/> which uses <see cref="DateTime.Now"/>.
    /// </summary>
    public class DateTimeNowSchedulingProvider : ISchedulingProvider
    {
        public TimeSpan Compute(DateTime executeAt)
        {
            return executeAt.Subtract(DateTime.Now);
        }
    }
}
