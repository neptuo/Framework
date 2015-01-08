using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Events.Handlers
{
    /// <summary>
    /// Registers event handler using factory method.
    /// </summary>
    /// <typeparam name="TEvent">Type of event data.</typeparam>
    public class GetterEventHandlerFactory<TEvent> : IEventHandlerFactory<TEvent>
    {
        /// <summary>
        /// Factory method for event handlers.
        /// </summary>
        public Func<IEventHandler<TEvent>> Getter { get; private set; }

        /// <summary>
        /// Creates new instance using <paramref name="getter"/>.
        /// </summary>
        /// <param name="getter">Factory method for event handlers.</param>
        public GetterEventHandlerFactory(Func<IEventHandler<TEvent>> getter)
        {
            Guard.NotNull(getter, "getter");
            Getter = getter;
        }

        public IEventHandler<TEvent> CreateHandler(TEvent eventData, IEventManager currentManager)
        {
            return Getter();
        }
    }
}
