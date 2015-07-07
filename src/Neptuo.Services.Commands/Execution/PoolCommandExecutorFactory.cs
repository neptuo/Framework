using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Commands.Execution
{
    /// <summary>
    /// Executed commands in pool (with max of concurrently executed commands).
    /// </summary>
    public class PoolCommandExecutorFactory : ICommandExecutorFactory, IPoolCommandExecutorContext
    {
        /// <summary>
        /// List waiting commands.
        /// </summary>
        protected internal Queue<object> CommandQueue { get; private set; }

        /// <summary>
        /// Max pool size. If null, pool is unlimited.
        /// </summary>
        protected internal int? PoolSize { get; private set; }

        /// <summary>
        /// Factory for inner executors.
        /// </summary>
        protected internal ICommandExecutorFactory InnerFactory { get; private set; }

        /// <summary>
        /// List of pooled inner executors.
        /// </summary>
        protected internal HashSet<ICommandExecutor> InnerExecutors { get; private set; }

        /// <summary>
        /// Flag to see if execution of commands is locked.
        /// </summary>
        protected bool IsUseLocking { get; private set; }

        /// <summary>
        /// Flag to see if there is waiting command (CommandQueue is not empty).
        /// </summary>
        protected bool HasNextCommand
        {
            get { return CommandQueue.Count > 0; }
        }

        /// <summary>
        /// Creates pool excutor factory with unlimited pool size.
        /// </summary>
        /// <param name="innerFactory">Factory for creating executors inside pool.</param>
        /// <param name="isUseLocking">Whether runs in multithreaded context.</param>
        public PoolCommandExecutorFactory(ICommandExecutorFactory innerFactory, bool isUseLocking = true)
        {
            Ensure.NotNull(innerFactory, "innerFactory");
            PoolSize = null;
            InnerFactory = innerFactory;
            CommandQueue = new Queue<object>();
            InnerExecutors = new HashSet<ICommandExecutor>();
            IsUseLocking = isUseLocking;
        }

        /// <summary>
        /// Creates pool excutor factory with pool size of <paramref name="poolSize"/>.
        /// </summary>
        /// <param name="poolSize">Pool size (max of concurrently executed commands).</param>
        /// <param name="innerFactory">Factory for creating executors inside pool.</param>
        /// <param name="isUseLocking">Whether runs in multithreaded context.</param>
        public PoolCommandExecutorFactory(int poolSize, ICommandExecutorFactory innerFactory, bool isUseLocking = true)
            : this(innerFactory, isUseLocking)
        {
            Ensure.Positive(poolSize, "poolSize");
            PoolSize = poolSize;
        }

        /// <summary>
        /// Creates pool executor. Calls <see cref="CreatePoolExecutor"/>.
        /// </summary>
        /// <param name="command">Command to handle.</param>
        /// <returns>Poo executor.</returns>
        public ICommandExecutor CreateExecutor(object command)
        {
            return CreatePoolExecutor(command);
        }

        /// <summary>
        /// Creates <see cref="PoolCommandExecutor"/>. Virtual for overriding in derivered classes.
        /// </summary>
        /// <param name="command">Command to handle.</param>
        /// <returns>Pool executor.</returns>
        protected virtual PoolCommandExecutor CreatePoolExecutor(object command)
        {
            return new PoolCommandExecutor(this);
        }

        #region IPoolCommandExecutorContext

        /// <summary>
        /// Flag to see if execution of next command is possible.
        /// True if pool limit is not reached and there is waiting command.
        /// </summary>
        public bool IsNextAvailable
        {
            get
            {
                bool hasCommands = HasNextCommand;
                if (PoolSize == null)
                    return hasCommands;

                return InnerExecutors.Count < PoolSize && hasCommands;
            }
        }

        /// <summary>
        /// Adds command to queue.
        /// </summary>
        /// <param name="command">Command to enqueue.</param>
        public void AddCommand(object command)
        {
            Ensure.NotNull(command, "command");
            CommandQueue.Enqueue(command);
        }

        /// <summary>
        /// Gets next command to handle or throws <see cref="InvalidOperationException"/> if queue is empty.
        /// Before calling this, call <see cref="IsNextAvailable"/>.
        /// </summary>
        /// <returns></returns>
        public object NextCommand()
        {
            if (!HasNextCommand)
                throw new InvalidOperationException("Command queue is empty.");

            return CommandQueue.Dequeue();
        }

        /// <summary>
        /// Creates and enqueues inner executor.
        /// </summary>
        /// <param name="command">Command to handle.</param>
        /// <returns>Inner executor.</returns>
        public ICommandExecutor CreateInnerExecutor(object command)
        {
            ICommandExecutor executor = InnerFactory.CreateExecutor(command);
            InnerExecutors.Add(executor);
            return executor;
        }

        /// <summary>
        /// Removes executor from queue.
        /// </summary>
        /// <param name="executor">Finished executor.</param>
        public void RemoveDoneExecutor(ICommandExecutor executor)
        {
            InnerExecutors.Remove(executor);
        }

        /// <summary>
        /// Executes <paramref name="action"/> in locked mode (if locking is enabled).
        /// </summary>
        /// <param name="action">Action to run.</param>
        public void ExecuteLocked(Action action)
        {
            Ensure.NotNull(action, "action");
            if (IsUseLocking)
            {
                lock (CommandQueue)
                    lock (InnerExecutors)
                    {
                        action();
                    }
            }
            else
            {
                action();
            }
        }

        #endregion
    }
}
