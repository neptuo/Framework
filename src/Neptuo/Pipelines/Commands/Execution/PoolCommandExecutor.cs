using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Pipelines.Commands.Execution
{
    /// <summary>
    /// Executes commands in context of pool.
    /// When size of pool is reached, execution of next commands wait until previous command are done.
    /// </summary>
    public class PoolCommandExecutor : ICommandExecutor
    {
        /// <summary>
        /// Current pool context.
        /// </summary>
        protected IPoolCommandExecutorContext PoolContext { get; private set; }

        public event Action<ICommandExecutor, object> OnCommandHandled;

        public PoolCommandExecutor(IPoolCommandExecutorContext poolContext)
        {
            Guard.NotNull(poolContext, "poolContext");
            PoolContext = poolContext;
        }

        public void Handle(object command)
        {
            PoolContext.AddCommand(command);
            HandleCommandIfPossible();
        }

        /// <summary>
        /// Handles next command if pool size is not reached and command queue is not empty.
        /// </summary>
        private void HandleCommandIfPossible()
        {
            if (PoolContext.IsNextAvailable)
            {
                PoolContext.ExecuteLocked(() =>
                {
                    if (PoolContext.IsNextAvailable)
                    {
                        object command = PoolContext.NextCommand();
                        ICommandExecutor executor = PoolContext.CreateInnerExecutor(command);
                        executor.OnCommandHandled += OnExecutorCommandHandled;
                        DoHandleCommand(executor, command);
                    }
                });
            }
        }

        /// <summary>
        /// Just calls handle on executor.
        /// </summary>
        /// <param name="executor">Executor to run.</param>
        /// <param name="command">Command to process.</param>
        protected virtual void DoHandleCommand(ICommandExecutor executor, object command)
        {
            executor.Handle(command);
        }

        /// <summary>
        /// Raised when inner executor finishes his job on command.
        /// </summary>
        /// <param name="executor">Inner executor.</param>
        /// <param name="command">Handled command.</param>
        private void OnExecutorCommandHandled(ICommandExecutor executor, object command)
        {
            if (OnCommandHandled != null)
                OnCommandHandled(this, command);

            PoolContext.RemoveDoneExecutor(executor);
            HandleCommandIfPossible();
        }
    }
}
