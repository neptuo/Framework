using Neptuo.Events.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Events
{
    public class EventManager : IEventManager, IEventDispatcher, IEventRegistry
    {
        private Dictionary<Type, List<object>> Registry { get; set; }

        public EventManager()
        {
            Registry = new Dictionary<Type, List<object>>();
        }

        public void Publish<TEvent>(TEvent eventData)
        {
            if (eventData == null)
                throw new ArgumentNullException("eventData");

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

        public void Subscribe<TEvent>(IEventHandlerFactory<TEvent> factory)
        {
            Type eventType = typeof(TEvent);
            List<object> handlers;
            if (!Registry.TryGetValue(eventType, out handlers))
            {
                handlers = new List<object>();
                Registry.Add(eventType, handlers);
            }

            handlers.Add(factory);
        }

        public void UnSubscribe<TEvent>(IEventHandlerFactory<TEvent> factory)
        {
            Type eventType = typeof(TEvent);
            List<object> handlers;
            if (Registry.TryGetValue(eventType, out handlers))
                handlers.Remove(factory);
        }
    }
}
