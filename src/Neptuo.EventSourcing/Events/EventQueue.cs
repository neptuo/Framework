using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Events
{
    public class EventQueue
    {
        private readonly ConcurrentQueue<IEvent> storage = new ConcurrentQueue<IEvent>();

        public void Push(IEvent e)
        {
            Ensure.NotNull(e, "e");
            storage.Enqueue(e);
        }

        public bool TryPop(out IEvent e)
        {
        }
    }
}
