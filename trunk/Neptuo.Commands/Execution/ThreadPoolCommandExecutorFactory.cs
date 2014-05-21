using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands.Execution
{
    /// <summary>
    /// Executes commands pool of threads.
    /// </summary>
    public class ThreadPoolCommandExecutorFactory : PoolCommandExecutorFactory
    {
        /// <summary>
        /// Creates thread pool excutor factory with unlimited pool size.
        /// </summary>
        /// <param name="innerFactory">Factory for creating executors inside pool.</param>
        public ThreadPoolCommandExecutorFactory(ICommandExecutorFactory innerFactory)
            : base(innerFactory)
        { }

        /// <summary>
        /// Creates thread pool excutor factory with pool size of <paramref name="poolSize"/>.
        /// </summary>
        /// <param name="poolSize">Pool size (max of concurrently executed commands).</param>
        /// <param name="innerFactory">Factory for creating executors inside pool.</param>
        public ThreadPoolCommandExecutorFactory(int poolSize, ICommandExecutorFactory innerFactory)
            : base(poolSize, innerFactory)
        { }

        protected override PoolCommandExecutor CreatePoolExecutor(object command)
        {
            return new ThreadPoolCommandExecutor(this);
        }
    }
}
