using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands
{
    /// <summary>
    /// The date and time provider for scheduling delayed commands.
    /// </summary>
    public interface ICommandSchedulingProvider
    {
        /// <summary>
        /// Computes a delay after which command will be executed.
        /// </summary>
        /// <param name="executeAt">The date and time when execution should be done.</param>
        /// <returns>A delay after which command will be executed.</returns>
        TimeSpan Compute(DateTime executeAt);
    }
}
