﻿using Neptuo.ComponentModel;
using Neptuo.Pipelines.Events;
using Neptuo.Pipelines.Events.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Pipelines.Events
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