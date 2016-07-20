using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neptuo.Internals
{
    internal class CommandThreadPool : DisposableBase
    {
        private readonly TreeQueue queue;
        private readonly List<Tuple<TreeQueue.Queue, Thread>> threads = new List<Tuple<TreeQueue.Queue, Thread>>();

        public CommandThreadPool(TreeQueue queue)
        {
            Ensure.NotNull(queue, "queue");
            this.queue = queue;
            this.queue.QueueAdded += OnQueueAdded;
        }

        private void OnQueueAdded(TreeQueue.Queue queue)
        {
            Thread thread = new Thread(OnThread);
            thread.Start(queue);
            threads.Add(new Tuple<TreeQueue.Queue, Thread>(queue, thread));
        }

        private void OnThread(object parameter)
        {
            TreeQueue.Queue queue = (TreeQueue.Queue)parameter;

            bool isNew = queue.Count > 0;
            queue.ItemAdded += (q, i) => isNew = true;

            while (true)
            {
                if (isNew)
                {
                    Func<Task> execute = null;
                    lock (queue.Lock)
                    {
                        execute = queue.Dequeue();
                        isNew = queue.Count > 0;
                    }

                    if (execute != null)
                        execute().Wait();
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
        }

        protected override void DisposeUnmanagedResources()
        {
            base.DisposeUnmanagedResources();

            foreach (Tuple<TreeQueue.Queue, Thread> thread in threads)
                thread.Item2.Abort();
        }
    }
}
