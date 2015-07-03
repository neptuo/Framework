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
    /// Common extensions for <see cref="IEventHandlerCollection"/>.
    /// </summary>
    public static class _EventHandlerCollectionExtensions
    {
        /// <summary>
        /// Registers <paramref name="eventHandler"/> to by notified for events of type <typeparamref name="TEvent"/>
        /// in the time of life of returned disposable object.
        /// </summary>
        /// <typeparam name="TEvent">Type of event data.</typeparam>
        /// <param name="collection">Target event registry.</param>
        /// <param name="eventHandler">Event handler.</param>
        /// <returns>Subscription lifetime manager.</returns>
        public static IDisposable Using<TEvent>(this IEventHandlerCollection collection, IEventHandler<TEvent> eventHandler)
        {
            return new UsignEventHandlerSubscriber<TEvent>(collection, eventHandler);
        }

        public static IEventHandlerCollection SubscribeAll(this IEventHandlerCollection collection, object handler)
        {
            Ensure.NotNull(collection, "eventRegistry");
            Ensure.NotNull(handler, "handler");

            Type genericHandlerType = typeof(IEventHandler<>);
            foreach (Type interfaceType in handler.GetType().GetInterfaces())
            {
                if (interfaceType.IsGenericType && genericHandlerType.IsAssignableFrom(interfaceType))
                {
                    Type[] arguments = interfaceType.GetGenericArguments();
                    if (arguments.Length != 1)
                        continue;

                    string subscribeName = TypeHelper.MethodName<IEventHandlerCollection, IEventHandler<object>, IEventHandlerCollection>(c => c.Subscribe);
                    MethodInfo subscribe = collection.GetType().GetMethod(subscribeName).MakeGenericMethod(arguments[0]);
                    subscribe.Invoke(collection, new object[] { handler });
                }
            }

            return collection;
        }
    }

    internal class UsignEventHandlerSubscriber<TEvent> : DisposableBase
    {
        private IEventHandlerCollection eventRegistry;
        private IEventHandler<TEvent> eventHandler;

        public UsignEventHandlerSubscriber(IEventHandlerCollection eventRegistry, IEventHandler<TEvent> eventHandler)
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
