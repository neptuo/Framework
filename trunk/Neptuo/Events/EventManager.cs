using Neptuo.Events.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Events
{
    /// <summary>
    /// Implementation of <see cref="IEventManager"/>.
    /// </summary>
    public class EventManager : IEventManager, IEventDispatcher, IEventRegistry
    {
        /// <summary>
        /// Internal storage for registrations.
        /// </summary>
        protected Dictionary<Type, List<object>> Registry { get; set; }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        public EventManager()
        {
            Registry = new Dictionary<Type, List<object>>();
        }

        public void Publish<TEvent>(TEvent eventData)
        {
            Guard.NotNull(eventData, "eventData");

            Type eventType = typeof(TEvent);
            List<object> handlerFactories;
            if (Registry.TryGetValue(eventType, out handlerFactories))
            {
                foreach (IEventHandlerFactory<TEvent> handlerFactory in handlerFactories.ToList())
                {
                    IEventHandler<TEvent> handler = handlerFactory.CreateHandler(eventData, this);
                    if(handler != null)
                        handler.Handle(eventData);
                }
            }
        }

        /// <summary>
        /// Subscribes <paramref name="factory"/> for events of type <paramref name="eventDataType"/>.
        /// Doesn't provide any type checks!
        /// </summary>
        /// <param name="eventDataType">Type of event data.</param>
        /// <param name="factory">Event handler factory for events of type <paramref name="eventDataType"/>.</param>
        protected void SubscribeInternal(Type eventDataType, IEventHandlerFactory factory)
        {
            List<object> handlers;
            if (!Registry.TryGetValue(eventDataType, out handlers))
            {
                handlers = new List<object>();
                Registry.Add(eventDataType, handlers);
            }

            handlers.Add(factory);
        }

        public void Subscribe<TEvent>(IEventHandlerFactory<TEvent> factory)
        {
            Guard.NotNull(factory, "factory");
            Type eventDataType = typeof(TEvent);
            SubscribeInternal(eventDataType, factory);
        }

        public void Subscribe(Type eventDataType, IEventHandlerFactory factory)
        {
            Guard.NotNull(eventDataType, "eventDataType");
            Guard.NotNull(factory, "factory");

            if (!typeof(IEventHandlerFactory<>).MakeGenericType(eventDataType).IsAssignableFrom(factory.GetType()))
                throw Guard.Exception.Argument("factory", "Factory doesn't implement IEventHandlerFactory<{0}>", eventDataType.FullName);

            SubscribeInternal(eventDataType, factory);
        }

        /// <summary>
        /// Unsubscribes <paramref name="factory"/> from events of type <paramref name="eventDataType"/>.
        /// Doesn't provide any type checks!
        /// </summary>
        /// <param name="eventDataType">Type of event data.</param>
        /// <param name="factory">Event handler factory for events of type <paramref name="eventDataType"/>.</param>
        protected void UnSubscribeInternal(Type eventDataType, IEventHandlerFactory factory)
        {
            List<object> handlers;
            if (Registry.TryGetValue(eventDataType, out handlers))
                handlers.Remove(factory);
        }

        public void UnSubscribe<TEvent>(IEventHandlerFactory<TEvent> factory)
        {
            Guard.NotNull(factory, "factory");
            Type eventDataType = typeof(TEvent);
            UnSubscribeInternal(eventDataType, factory);
        }

        public void UnSubscribe(Type eventDataType, IEventHandlerFactory factory)
        {
            Guard.NotNull(eventDataType, "eventDataType");
            Guard.NotNull(factory, "factory");
            UnSubscribeInternal(eventDataType, factory);
        }
    }
}
