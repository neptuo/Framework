using Neptuo.Jobs.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Events
{
    public class EventPublishBackgroundHandler : IBackgroundHandler
    {
        private readonly EventQueue queue;

        public EventPublishBackgroundHandler(EventQueue queue)
        {
            Ensure.NotNull(queue, "queue");
            this.queue = queue;
        }

        public void Invoke()
        {
            IEvent e;
            if (queue.TryPop(out e))
            {
                // TODO: Walk through handlers for particular event type. Run each handler and if succeeds, save flag that handler H handled event E.
                // (This - per handler event handled flags - ensures that if an application crashes, after restart only the remaining handlers get fired.)
            }
        }
    }
}
