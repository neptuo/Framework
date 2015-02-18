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
    public class DefaultEventManager : IEventDispatcher, IEventRegistry
    {
        /// <summary>
        /// Internal storage for registrations.
        /// </summary>
        protected Dictionary<Type, List<object>> Registry { get; private set; }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        public DefaultEventManager()
        {
            Registry = new Dictionary<Type, List<object>>();
        }

        public Task PublishAsync<TEvent>(TEvent payload)
        {
            Guard.NotNull(payload, "payload");

            Type eventType = typeof(TEvent);
            List<object> handlers;

            if (Registry.TryGetValue(eventType, out handlers))
            {
                Task[] tasks = new Task[handlers.Count];

                for (int i = 0; i < handlers.Count; i++)
                    tasks[i] = ((IEventHandler<TEvent>)handlers[i]).HandleAsync(payload);

                return Task.Factory.ContinueWhenAll(tasks, (items) => Task.FromResult(true));
            }

            return Task.FromResult(false);
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
