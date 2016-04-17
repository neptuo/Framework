using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neptuo.Events.Handlers
{
    /// <summary>
    /// Common extensions for <see cref="IEventHandlerCollection"/>.
    /// </summary>
    public static class _EventHandlerCollectionExtensions
    {
        /// <summary>
        /// Returns continuation task that is completed when <paramref name="eventHandlers"/> gets
        /// notification for event of the type <paramref name="T"/> and <paramref name="filter"/> is satisfied.
        /// </summary>
        /// <typeparam name="T">The type of the event (can contain <see cref="Envelope"/> or <see cref="IEventHandleContext{T}"/>).</typeparam>
        /// <param name="eventHandlers">The collection of an event handlers.</param>
        /// <param name="filter">The event filter, only the event for which returns true is used to complete the task.</param>
        /// <param name="token">The continuation task cancellation token.</param>
        /// <returns>
        /// Continuation task that is completed when <paramref name="eventHandlers"/> gets
        /// notification for event of the type <paramref name="T"/> and <paramref name="filter"/> is satisfied.
        /// </returns>
        public static Task<T> Await<T>(this IEventHandlerCollection eventHandlers, Func<T, bool> filter, CancellationToken token)
        {
            Ensure.NotNull(eventHandlers, "eventHandlers");
            Ensure.NotNull(filter, "filter");
            Ensure.NotNull(token, "token");
            return Task.Factory.StartNew(() =>
            {
                T result = default(T);

                IEventHandler<T> handler = DelegateEventHandler.FromAction<T>(payload => result = payload);

                eventHandlers.Add(handler);

                while (result == null || !filter(result))
                    Thread.Sleep(50);

                eventHandlers.Remove(handler);
                return result;
            }, token);
        }

        /// <summary>
        /// Returns continuation task that is completed when <paramref name="eventHandlers"/> gets
        /// notification for event of the type <paramref name="T"/> and <paramref name="filter"/> is satisfied.
        /// </summary>
        /// <typeparam name="T">The type of the event (can contain <see cref="Envelope"/> or <see cref="IEventHandleContext{T}"/>).</typeparam>
        /// <param name="eventHandlers">The collection of an event handlers.</param>
        /// <param name="filter">The event filter, only the event for which returns true is used to complete the task.</param>
        /// <returns>
        /// Continuation task that is completed when <paramref name="eventHandlers"/> gets
        /// notification for event of the type <paramref name="T"/> and <paramref name="filter"/> is satisfied.
        /// </returns>
        public static Task<T> Await<T>(this IEventHandlerCollection eventHandlers, Func<T, bool> filter)
        {
            return Await(eventHandlers, filter, CancellationToken.None);
        }

        /// <summary>
        /// Returns continuation task that is completed when <paramref name="eventHandlers"/> gets
        /// notification for event of the type <paramref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the event (can contain <see cref="Envelope"/> or <see cref="IEventHandleContext{T}"/>).</typeparam>
        /// <param name="eventHandlers">The collection of an event handlers.</param>
        /// <param name="token">The continuation task cancellation token.</param>
        /// <returns>
        /// Continuation task that is completed when <paramref name="eventHandlers"/> gets
        /// notification for event of the type <paramref name="T"/>.
        /// </returns>
        public static Task<T> Await<T>(this IEventHandlerCollection eventHandlers, CancellationToken token)
        {
            return Await<T>(eventHandlers, e => true, token);
        }

        /// <summary>
        /// Returns continuation task that is completed when <paramref name="eventHandlers"/> gets
        /// notification for event of the type <paramref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the event (can contain <see cref="Envelope"/> or <see cref="IEventHandleContext{T}"/>).</typeparam>
        /// <param name="eventHandlers">The collection of an event handlers.</param>
        /// <returns>
        /// Continuation task that is completed when <paramref name="eventHandlers"/> gets
        /// notification for event of the type <paramref name="T"/>.
        /// </returns>
        public static Task<T> Await<T>(this IEventHandlerCollection eventHandlers)
        {
            return Await<T>(eventHandlers, CancellationToken.None);
        }
    }
}
