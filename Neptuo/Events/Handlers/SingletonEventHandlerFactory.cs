using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Events.Handlers
{
    /// <summary>
    /// Single instance event handler factory.
    /// </summary>
    /// <typeparam name="TEvent">Type of event data.</typeparam>
    public class SingletonEventHandlerFactory<TEvent> : IEventHandlerFactory<TEvent>
    {
        /// <summary>
        /// Singleton event handler.
        /// </summary>
        public IEventHandler<TEvent> Handler { get; private set; }

        /// <summary>
        /// Creates new instance using <paramref name="handler"/>.
        /// </summary>
        /// <param name="handler">Singleton event handler.</param>
        public SingletonEventHandlerFactory(IEventHandler<TEvent> handler)
        {
            Guard.NotNull(handler, "handler");
            Handler = handler;
        }

        public IEventHandler<TEvent> CreateHandler(TEvent eventData, IEventManager currentManager)
        {
            return Handler;
        }
    }
}
