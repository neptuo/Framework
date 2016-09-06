using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neptuo.Internals
{
    internal class TreeQueueThreadPool : DisposableBase
    {
        private readonly TreeQueue queue;

        private readonly object threadsLock = new object();
        private readonly List<Tuple<TreeQueue.Queue, bool>> threads = new List<Tuple<TreeQueue.Queue, bool>>();

        public TreeQueueThreadPool(TreeQueue queue)
        {
            Ensure.NotNull(queue, "queue");
            this.queue = queue;
            this.queue.QueueAdded += OnQueueAdded;
        }

        private void OnQueueAdded(TreeQueue.Queue queue)
        {
            lock (threadsLock)
            {
                queue.ItemAdded += OnQueueItemAdded;
                threads.Add(new Tuple<TreeQueue.Queue, bool>(queue, false));
                OnQueueItemAdded(queue, null);
            }
        }

        private void OnQueueItemAdded(TreeQueue.Queue item, Func<Task> execute)
        {
            Tuple<TreeQueue.Queue, bool> thread = null;
            lock (threadsLock)
                thread = threads.FirstOrDefault(t => t.Item1 == item);

            if (thread == null)
            {
                // TODO: This is weird.
                return;
            }

            // If thread is bussy, do nothing.
            if (thread.Item2 || thread.Item1.Count == 0)
                return;

            execute = thread.Item1.Dequeue();
            execute().ContinueWith(OnQueueItemCompleted, thread);
        }

        private void OnQueueItemCompleted(Task t, object state)
        {
            // Thread has completed it's work.

            Tuple<TreeQueue.Queue, bool> thread = (Tuple<TreeQueue.Queue, bool>)state;
            if (thread.Item1.Count > 0)
            {
                // If there is new work, do it now.
                Func<Task> execute = thread.Item1.Dequeue();
                execute().ContinueWith(OnQueueItemCompleted, thread);
            }
            else
            {
                // Otherwise is it free for new work.
                thread.Item2 = false;
            }
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
            // TODO: Cancel all currently running tasks.
        }
    }
}
