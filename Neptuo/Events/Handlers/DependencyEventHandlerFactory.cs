using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Events.Handlers
{
    /// <summary>
    /// Creates instances of event handler using <see cref="IDependencyProvider"/>.
    /// </summary>
    /// <typeparam name="TEvent">Type of event.</typeparam>
    /// <typeparam name="TEventHandler">Type of handler to handle events of type <typeparamref name="TEvent"/>.</typeparam>
    public class DependencyEventHandlerFactory<TEvent, TEventHandler> : IEventHandlerFactory<TEvent>
        where TEventHandler : IEventHandler<TEvent>
    {
        /// <summary>
        /// Current dependency provider for creating instances of <typeparamref name="TEventHandler" />.
        /// </summary>
        private IDependencyProvider dependencyProvider;

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="dependencyProvider">Current dependency provider for creating instances of <typeparamref name="TEventHandler" />.</param>
        public DependencyEventHandlerFactory(IDependencyProvider dependencyProvider)
        {
            Guard.NotNull(dependencyProvider, "dependencyProvider");
            this.dependencyProvider = dependencyProvider;
        }

        /// <summary>
        /// Creates instance of <typeparamref name="TEventHandler"/>.
        /// </summary>
        /// <param name="eventData">Event to handle.</param>
        /// <param name="currentManager">Current event manager.</param>
        /// <returns>Instance of <typeparamref name="TEventHandler"/>.</returns>
        public IEventHandler<TEvent> CreateHandler(TEvent eventData, IEventManager currentManager)
        {
            return dependencyProvider.Resolve<TEventHandler>();
        }
    }
}
