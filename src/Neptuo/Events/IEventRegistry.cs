using Neptuo.Events.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Events
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
        /// <param name="factory">Event handler factory.</param>
        void Subscribe<TEvent>(IEventHandlerFactory<TEvent> factory);

        /// <summary>
        /// Subscribes event handle factor for events of type <paramref name="eventDataType"/>.
        /// </summary>
        /// <param name="eventDataType">Type of event data.</typeparam>
        /// <param name="factory">Event handler factory.</param>
        void Subscribe(Type eventDataType, IEventHandlerFactory factory);

        /// <summary>
        /// Unsubscribes event handle factor from events of type <typeparamref name="TEvent"/>.
        /// </summary>
        /// <typeparam name="TEvent">Type of event data.</typeparam>
        /// <param name="factory">Event handler factory.</param>
        void UnSubscribe<TEvent>(IEventHandlerFactory<TEvent> factory);

        /// <summary>
        /// Unsubscribes event handle factor from events of type <paramref name="eventDataType"/>.
        /// </summary>
        /// <param name="eventDataType">Type of event data.</typeparam>
        /// <param name="factory">Event handler factory.</param>
        void UnSubscribe(Type eventDataType, IEventHandlerFactory factory);
    }
}
