using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Events.Handlers
{
    public class ActionEventHandler<TEvent> : IEventHandler<TEvent>
    {
        public Action<TEvent> Action { get; private set; }

        public ActionEventHandler(Action<TEvent> action)
        {
            Guard.NotNull(action, "action");
            Action = action;
        }

        public void Handle(TEvent eventData)
        {
            Action(eventData);
        }
    }
}
