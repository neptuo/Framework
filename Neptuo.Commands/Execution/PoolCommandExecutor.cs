using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands.Execution
{
    public class PoolCommandExecutor : ICommandExecutor
    {
        protected PoolCommandExecutorFactory PoolFactory { get; private set; }

        public event Action<ICommandExecutor, object> OnCommandHandled;

        public PoolCommandExecutor(PoolCommandExecutorFactory poolFactory)
        {
            Guard.NotNull(poolFactory, "poolFactory");
            PoolFactory = poolFactory;
        }

        public void Handle(object command)
        {
            PoolFactory.CommandQueue.Enqueue(command);
            HandleCommandIfPossible();
        }

        private void HandleCommandIfPossible()
        {
            if (PoolFactory.IsNextAvailable)
            {
                PoolFactory.ExecuteLocked(() =>
                {
                    if (PoolFactory.IsNextAvailable)
                    {
                        object command = PoolFactory.CommandQueue.Dequeue();
                        ICommandExecutor executor = PoolFactory.InnerFactory.CreateExecutor(command);
                        PoolFactory.Executors.Add(executor);
                        executor.OnCommandHandled += OnExecutorCommandHandled;
                        DoHandleCommand(executor, command);
                    }
                });
            }
        }

        protected virtual void DoHandleCommand(ICommandExecutor executor, object command)
        {
            executor.Handle(command);
        }

        private void OnExecutorCommandHandled(ICommandExecutor executor, object command)
        {
            if (OnCommandHandled != null)
                OnCommandHandled(this, command);

            PoolFactory.Executors.Remove(executor);
            HandleCommandIfPossible();
        }
    }
}
