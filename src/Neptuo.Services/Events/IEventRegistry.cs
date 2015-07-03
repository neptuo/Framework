using Neptuo.Services.Events.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Events
{
    /// <summary>
    /// Provides methods for registering and unregistering event handlers.
    /// </summary>
    public interface IEventRegistry
    {
        /// <summary>
        /// Subscribes <paramref name="handler"/> for events of type <typeparamref name="TEvent"/>.
        /// </summary>
        /// <typeparam name="TEvent">Type of event data.</typeparam>
        /// <param name="handler">Event handler.</param>
        IEventRegistry Subscribe<TEvent>(IEventHandler<TEvent> handler);

        /// <summary>
        /// Unsubscribes <paramref name="handler"/> from events of type <typeparamref name="TEvent"/>.
        /// </summary>
        /// <typeparam name="TEvent">Type of event data.</typeparam>
        /// <param name="handler">Event handler.</param>
        IEventRegistry UnSubscribe<TEvent>(IEventHandler<TEvent> handler);
    }
}
