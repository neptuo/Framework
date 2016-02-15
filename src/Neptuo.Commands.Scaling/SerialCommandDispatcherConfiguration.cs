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
        public const int DefaultThreadSleepMilliseconds = 200;

        /// <summary>
        /// The number of milliseconds to suspend the worker thread before checking for new commands.
        /// </summary>
        public int ThreadSleepMilliseconds { get; private set; }

        /// <summary>
        /// Creates new instance with default value <see cref="DefaultThreadSleepMilliseconds"/>.
        /// </summary>
        public SerialCommandDispatcherConfiguration()
            : this(DefaultThreadSleepMilliseconds)
        { }

        /// <summary>
        /// Creates new instance with defined thread sleep interval for looking up commands.
        /// </summary>
        /// <param name="threadSleepMilliseconds"></param>
        public SerialCommandDispatcherConfiguration(int threadSleepMilliseconds)
        {
            Ensure.Positive(threadSleepMilliseconds, "threadSleepMilliseconds");
            ThreadSleepMilliseconds = threadSleepMilliseconds;
        }
    }
}
