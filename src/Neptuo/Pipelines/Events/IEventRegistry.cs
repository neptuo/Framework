using Neptuo.Pipelines.Events.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Pipelines.Events
{
    /// <summary>
    /// Provides methods for registering and unregistering event handlers.
    /// </summary>
    public interface IEventRegistry
    {
        /// <summary>
        /// Subscribes event handle factor for events of type <typeparamref name="TEvent"/>.
        /// </summary>
        /// <typeparam name="TEvent">Type of event data.</typeparam>
        /// <param name="handler">Event handler.</param>
        void Subscribe<TEvent>(IEventHandler<TEvent> handler);

        /// <summary>
        /// Unsubscribes event handle factor from events of type <typeparamref name="TEvent"/>.
        /// </summary>
        /// <typeparam name="TEvent">Type of event data.</typeparam>
        /// <param name="handler">Event handler.</param>
        void UnSubscribe<TEvent>(IEventHandler<TEvent> handler);
    }
}
