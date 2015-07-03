using Neptuo.ComponentModel;
using Neptuo.Linq.Expressions;
using Neptuo.Services.Events;
using Neptuo.Services.Events.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Events
{
    /// <summary>
    /// Common extensions for <see cref="IEventRegistry"/>.
    /// </summary>
    public static class _EventRegistryExtensions
    {
        /// <summary>
        /// Registers <paramref name="eventHandler"/> to by notified for events of type <typeparamref name="TEvent"/>
        /// in the time of life of returned disposable object.
        /// </summary>
        /// <typeparam name="TEvent">Type of event data.</typeparam>
        /// <param name="eventRegistry">Target event registry.</param>
        /// <param name="eventHandler">Event handler.</param>
        /// <returns>Subscription lifetime manager.</returns>
        public static IDisposable Using<TEvent>(this IEventRegistry eventRegistry, IEventHandler<TEvent> eventHandler)
        {
            return new UsignEventHandlerSubscriber<TEvent>(eventRegistry, eventHandler);
        }

        public static IEventRegistry SubscribeAll(this IEventRegistry eventRegistry, object handler)
        {
            Ensure.NotNull(eventRegistry, "eventRegistry");
            Ensure.NotNull(handler, "handler");

            Type genericHandlerType = typeof(IEventHandler<>);
            foreach (Type interfaceType in handler.GetType().GetInterfaces())
            {
                if (interfaceType.IsGenericType && genericHandlerType.IsAssignableFrom(interfaceType))
                {
                    Type[] arguments = interfaceType.GetGenericArguments();
                    if (arguments.Length != 1)
                        continue;

                    string subscribeName = TypeHelper.MethodName<IEventRegistry, IEventHandler<object>, IEventRegistry>(c => c.Subscribe);
                    MethodInfo subscribe = eventRegistry.GetType().GetMethod(subscribeName).MakeGenericMethod(arguments[0]);
                    subscribe.Invoke(eventRegistry, new object[] { handler });
                }
            }

            return eventRegistry;
        }
    }

    internal class UsignEventHandlerSubscriber<TEvent> : DisposableBase
    {
        private IEventRegistry eventRegistry;
        private IEventHandler<TEvent> eventHandler;

        public UsignEventHandlerSubscriber(IEventRegistry eventRegistry, IEventHandler<TEvent> eventHandler)
        {
            this.eventRegistry = eventRegistry;
            this.eventHandler = eventHandler;

            eventRegistry.Subscribe(eventHandler);
        }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            eventRegistry.UnSubscribe(eventHandler);
        }
    }
}
