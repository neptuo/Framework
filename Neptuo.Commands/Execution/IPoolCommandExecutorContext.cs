using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands.Execution
{
    /// <summary>
    /// Context for <see cref="PoolCommandExecutor"/>.
    /// </summary>
    public interface IPoolCommandExecutorContext
    {
        /// <summary>
        /// Flag to see if execution of next command is possible.
        /// True if pool limit is not reached and there is waiting command.
        /// </summary>
        bool IsNextAvailable { get; }

        /// <summary>
        /// Adds command to queue.
        /// </summary>
        /// <param name="command">Command to enqueue.</param>
        void AddCommand(object command);

        /// <summary>
        /// Gets next command to handle or throws <see cref="InvalidOperationException"/> if queue is empty.
        /// Before calling this, call <see cref="IsNextAvailable"/>.
        /// </summary>
        /// <returns></returns>
        object NextCommand();

        /// <summary>
        /// Creates and enqueues inner executor.
        /// </summary>
        /// <param name="command">Command to handle.</param>
        /// <returns>Inner executor.</returns>
        ICommandExecutor CreateInnerExecutor(object command);

        /// <summary>
        /// Removes executor from queue.
        /// </summary>
        /// <param name="executor">Finished executor.</param>
        void RemoveDoneExecutor(ICommandExecutor executor);

        /// <summary>
        /// Executes <paramref name="action"/> in locked mode (if locking is enabled).
        /// </summary>
        /// <param name="action">Action to run.</param>
        void ExecuteLocked(Action action);
    }
}
