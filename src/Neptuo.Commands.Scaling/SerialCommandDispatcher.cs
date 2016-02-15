using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neptuo.Commands
{
    /// <summary>
    /// The implementation of <see cref="ICommandDispatcher"/> which executes all commands in serie (one by one - using queue).
    /// Commands are processed in separate thread and <see cref="HandleAsync"/> returns control right after storing command for dispatching.
    /// </summary>
    public class SerialCommandDispatcher : DisposableBase, ICommandDispatcher
    {
        private readonly Queue<object> storage = new Queue<object>();
        private readonly object storageLock = new object();
        private readonly Thread worker;
        private readonly ICommandDispatcher innerDispatcher;

        public SerialCommandDispatcher(ICommandDispatcher innerDispatcher)
        {
            Ensure.NotNull(innerDispatcher, "innerDispatcher");
            this.innerDispatcher = innerDispatcher;

            worker = new Thread(OnThread);
            worker.Start();
        }

        public Task HandleAsync<TCommand>(TCommand command)
        {
            Ensure.NotNull(command, "command");

            lock (storageLock)
                storage.Enqueue(command);

            return Task.FromResult(true);
        }

        private void OnThread(object parameter)
        {
            while (true)
            {
                bool isCommandProcessed = false;

                lock (storageLock)
                {
                    if (storage.Count > 0)
                    {
                        object command = storage.Dequeue();
                        isCommandProcessed = true;
                        innerDispatcher.HandleAsync(command).Wait();
                    }
                }

                if (!isCommandProcessed)
                    Thread.Sleep(100);
            }
        }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();

            IDisposable disposable = innerDispatcher as IDisposable;
            if (disposable != null)
                disposable.Dispose();
        }
    }
}
