using Neptuo.Events;
using Neptuo.Events.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Events
{
    public static class EventDispatcherExtensions
    {
        //public static Subscribe

        public static IDisposable Using<TEvent>(this IEventRegistry eventRegistry, IEventHandler<TEvent> eventHandler)
        {
            return new UsignEventHandlerSubscriber<TEvent>(eventRegistry, eventHandler);
        }
    }

    internal class UsignEventHandlerSubscriber<TEvent> : IDisposable
    {
        private IEventRegistry eventRegistry;
        private IEventHandlerFactory<TEvent> eventHandlerFactory;

        public UsignEventHandlerSubscriber(IEventRegistry eventRegistry, IEventHandler<TEvent> eventHandler)
        {
            this.eventRegistry = eventRegistry;
            this.eventHandlerFactory = new SingletonEventHandlerFactory<TEvent>(eventHandler);

            eventRegistry.Subscribe(eventHandlerFactory);
        }

        public void Dispose()
        {
            eventRegistry.UnSubscribe(eventHandlerFactory);
        }
    }

}
