using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Events.Handlers
{
    /// <summary>
    /// Event handler using delegate method.
    /// </summary>
    /// <typeparam name="TEvent">Type of event data.</typeparam>
    public class ActionEventHandler<TEvent> : IEventHandler<TEvent>
    {
        /// <summary>
        /// Degate for handling events.
        /// </summary>
        public Action<TEvent> Action { get; private set; }

        /// <summary>
        /// Creates new instance using <paramref name="action"/>.
        /// </summary>
        /// <param name="action">Degate for handling events.</param>
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
