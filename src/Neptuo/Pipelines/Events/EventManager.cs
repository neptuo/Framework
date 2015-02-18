using Neptuo.Pipelines.Events.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Pipelines.Events
{
    /// <summary>
    /// Default implementation of <see cref="IEventDispatcher"/> and <see cref="IEventRegistry"/>.
    /// </summary>
    public class EventManager : IEventDispatcher, IEventRegistry
    {
        /// <summary>
        /// Internal storage for registrations.
        /// </summary>
        protected Dictionary<Type, List<object>> Registry { get; private set; }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        public EventManager()
        {
            Registry = new Dictionary<Type, List<object>>();
        }

        public void Publish<TEvent>(TEvent payload)
        {
            Guard.NotNull(payload, "payload");

            Type eventType = typeof(TEvent);
            List<object> handlers;
            if (Registry.TryGetValue(eventType, out handlers))
            {
                foreach (IEventHandler<TEvent> handler in handlers)
                    handler.Handle(payload);
            }
        }

        public IEventRegistry Subscribe<TEvent>(IEventHandler<TEvent> handler)
        {
            Guard.NotNull(handler, "factory");
            Type eventType = typeof(TEvent);

            List<object> handlers;
            if (!Registry.TryGetValue(eventType, out handlers))
            {
                handlers = new List<object>();
                Registry.Add(eventType, handlers);
            }

            handlers.Add(handler);
            return this;
        }

        public IEventRegistry UnSubscribe<TEvent>(IEventHandler<TEvent> handler)
        {
            Guard.NotNull(handler, "factory");
            Type eventType = typeof(TEvent);

            List<object> handlers;
            if (Registry.TryGetValue(eventType, out handlers))
                handlers.Remove(handler);

            return this;
        }
    }
}
