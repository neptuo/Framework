using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands
{
    /// <summary>
    /// Configuration properties for <see cref="SerialCommandDispatcher"/>.
    /// </summary>
    public class SerialCommandDispatcherConfiguration
    {
        /// <summary>
        /// The number of milliseconds to suspend the worker thread before checking for new commands.
        /// </summary>
        public int ThreadSleepMilliseconds { get; private set; }
    }
}
