using Neptuo.ComponentModel;
using Neptuo.Activators;
using Neptuo.Events;
using Neptuo.Events.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Events
{
    public static class _EventDispatcherExtensions
    {
        /// <summary>
        /// Subscribes <typeparamref name="TEventHandler"/> using dependency handler factory.
        /// </summary>
        /// <typeparam name="TEvent">Type of event data.</typeparam>
        /// <typeparam name="TEventHandler">Type of event handler</typeparam>
        /// <param name="eventRegistry">Event registry.</param>
        /// <param name="dependencyProvider">Dependency provider for resolving .</param>
        /// <returns>Created event handler factory.</returns>
        public static DependencyEventHandlerFactory<TEvent, TEventHandler> SubscribeDependency<TEvent, TEventHandler>(this IEventRegistry eventRegistry, IDependencyProvider dependencyProvider)
            where TEventHandler : IEventHandler<TEvent>
        {
            Guard.NotNull(eventRegistry, "eventRegistry");
            Guard.NotNull(dependencyProvider, "dependencyProvider");

            DependencyEventHandlerFactory<TEvent, TEventHandler> factory = new DependencyEventHandlerFactory<TEvent, TEventHandler>(dependencyProvider);
            eventRegistry.Subscribe(factory);
            return factory;
        }

        public static IDisposable Using<TEvent>(this IEventRegistry eventRegistry, IEventHandler<TEvent> eventHandler)
        {
            return new UsignEventHandlerSubscriber<TEvent>(eventRegistry, eventHandler);
        }
    }

    internal class UsignEventHandlerSubscriber<TEvent> : DisposableBase
    {
        private IEventRegistry eventRegistry;
        private IEventHandlerFactory<TEvent> eventHandlerFactory;

        public UsignEventHandlerSubscriber(IEventRegistry eventRegistry, IEventHandler<TEvent> eventHandler)
        {
            this.eventRegistry = eventRegistry;
            this.eventHandlerFactory = new SingletonEventHandlerFactory<TEvent>(eventHandler);

            eventRegistry.Subscribe(eventHandlerFactory);
        }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            eventRegistry.UnSubscribe(eventHandlerFactory);
        }
    }
}
