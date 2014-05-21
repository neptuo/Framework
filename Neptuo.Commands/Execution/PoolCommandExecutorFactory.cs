using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands.Execution
{
    /// <summary>
    /// Executed commands in pool (with max of concurrently executed commands).
    /// </summary>
    public class PoolCommandExecutorFactory : ICommandExecutorFactory
    {
        protected internal Queue<object> CommandQueue { get; private set; }
        protected internal int? PoolSize { get; private set; }
        protected internal ICommandExecutorFactory InnerFactory { get; private set; }
        protected internal HashSet<ICommandExecutor> Executors { get; private set; }
        protected bool IsUseLocking { get; private set; }

        /// <summary>
        /// Creates pool excutor factory with unlimited pool size.
        /// </summary>
        /// <param name="innerFactory">Factory for creating executors inside pool.</param>
        /// <param name="isUseLocking">Whether runs in multithreaded context.</param>
        public PoolCommandExecutorFactory(ICommandExecutorFactory innerFactory, bool isUseLocking = true)
        {
            Guard.NotNull(innerFactory, "innerFactory");
            PoolSize = null;
            InnerFactory = innerFactory;
            CommandQueue = new Queue<object>();
            Executors = new HashSet<ICommandExecutor>();
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
            Guard.Positive(poolSize, "poolSize");
            PoolSize = poolSize;
        }

        public ICommandExecutor CreateExecutor(object command)
        {
            return CreatePoolExecutor(command);
        }

        protected virtual PoolCommandExecutor CreatePoolExecutor(object command)
        {
            return new PoolCommandExecutor(this);
        }

        #region Helpers

        internal void ExecuteLocked(Action action)
        {
            Guard.NotNull(action, "action");
            if (IsUseLocking)
            {
                lock (CommandQueue)
                lock (Executors)
                {
                    action();
                }
            }
            else
            {
                action();
            }
        }

        protected internal bool IsNextAvailable
        {
            get
            {
                bool hasCommands = CommandQueue.Count > 0;
                if (PoolSize == null)
                    return hasCommands;

                return Executors.Count < PoolSize && hasCommands;
            }
        }

        #endregion
    }
}
