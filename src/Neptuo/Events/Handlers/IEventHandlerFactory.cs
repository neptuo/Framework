using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Events.Handlers
{
    /// <summary>
    /// Marker interface for event handler factories.
    /// </summary>
    public interface IEventHandlerFactory
    { }

    /// <summary>
    /// Event handler factory for events of type <typeparamref name="TEvent"/>.
    /// </summary>
    /// <typeparam name="TEvent">Type of event data.</typeparam>
    public interface IEventHandlerFactory<TEvent> : IEventHandlerFactory
    {
        /// <summary>
        /// Creates event handler for <paramref name="eventData"/> of type <typeparamref name="TEvent"/>.
        /// </summary>
        /// <param name="eventData">Instance of event data.</param>
        /// <param name="currentManager">Current event manager that invokes this method.</param>
        /// <returns>Event handler for <paramref name="eventData"/>.</returns>
        IEventHandler<TEvent> CreateHandler(TEvent eventData, IEventManager currentManager);
    }
}
