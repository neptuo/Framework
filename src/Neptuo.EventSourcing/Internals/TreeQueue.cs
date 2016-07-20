using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Internals
{
    internal class TreeQueue
    {
        private readonly object storageLock = new object();
        private readonly Dictionary<object, Queue> storage = new Dictionary<object, Queue>();

        public event Action<Queue> QueueAdded;

        public void Enqueue(object distributor, Func<Task> execute)
        {
            Ensure.NotNull(distributor, "distributor");
            Ensure.NotNull(execute, "execute");

            Queue queue;
            if (!storage.TryGetValue(distributor, out queue))
            {
                lock (storageLock)
                {
                    if (!storage.TryGetValue(distributor, out queue))
                    {
                        storage[distributor] = queue = new Queue();
                        RaiseQueueAdded(queue);
                    }
                }
            }

            lock (queue.Lock)
            {
                queue.Enqueue(execute);
                queue.RaiseItemAdded(execute);
            }
        }

        private void RaiseQueueAdded(Queue queue)
        {
            if (QueueAdded != null)
                QueueAdded(queue);
        }

        public class Queue : Queue<Func<Task>>
        {
            public object Lock { get; private set; }
            public event Action<Queue, Func<Task>> ItemAdded;

            public Queue()
            {
                Lock = new object();
            }

            internal void RaiseItemAdded(Func<Task> item)
            {
                if(ItemAdded != null)
                    ItemAdded(this, item);
            }
        }
    }
}
