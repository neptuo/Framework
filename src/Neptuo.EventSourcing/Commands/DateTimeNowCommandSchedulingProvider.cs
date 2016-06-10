using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands
{
    /// <summary>
    /// The implementation of <see cref="ICommandSchedulingProvider"/> which uses <see cref="DateTime.Now"/>.
    /// </summary>
    public class DateTimeNowCommandSchedulingProvider : ICommandSchedulingProvider
    {
        public TimeSpan Compute(DateTime executeAt)
        {
            return executeAt.Subtract(DateTime.Now);
        }
    }
}
