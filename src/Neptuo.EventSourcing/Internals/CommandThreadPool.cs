using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neptuo.Internals
{
    public class CommandThreadPool : DisposableBase
    {
        private readonly TheeQueue queue;
        private readonly List<Tuple<TheeQueue.Queue, Thread>> threads = new List<Tuple<TheeQueue.Queue, Thread>>();

        public CommandThreadPool(TheeQueue queue)
        {
            Ensure.NotNull(queue, "queue");
            this.queue = queue;
            this.queue.QueueAdded += OnQueueAdded;
        }

        private void OnQueueAdded(TheeQueue.Queue queue)
        {
            Thread thread = new Thread(OnThread);
            thread.Start(queue);
            threads.Add(new Tuple<TheeQueue.Queue, Thread>(queue, thread));
        }

        private void OnThread(object parameter)
        {
            TheeQueue.Queue queue = (TheeQueue.Queue)parameter;

            bool isNew = false;
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

            foreach (Tuple<TheeQueue.Queue, Thread> thread in threads)
                thread.Item2.Abort();
        }
    }
}
