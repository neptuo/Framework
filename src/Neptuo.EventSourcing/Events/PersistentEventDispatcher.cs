﻿using Neptuo.Data;
using Neptuo.Events.Handlers;
using Neptuo.Formatters;
using Neptuo.Internals;
using Neptuo.Linq.Expressions;
using Neptuo.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Events
{
    /// <summary>
    /// The implementation of <see cref="IEventDispatcher"/> and <see cref="IEventHandlerCollection"/> with persistent delivery.
    /// </summary>
    public class PersistentEventDispatcher : IEventDispatcher, IEventHandlerCollection
    {
        private readonly Dictionary<Type, HashSet<HandlerDescriptor>> storage = new Dictionary<Type, HashSet<HandlerDescriptor>>();
        private readonly IEventPublishingStore eventStore;
        private readonly HandlerDescriptorProvider descriptorProvider;

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="store">The publishing store for command persistent delivery.</param>
        public PersistentEventDispatcher(IEventPublishingStore store)
        {
            Ensure.NotNull(store, "store");
            this.eventStore = store;
            this.descriptorProvider = new HandlerDescriptorProvider(
                typeof(IEventHandler<>),
                typeof(IEventHandlerContext<>),
                TypeHelper.MethodName<IEventHandler<object>, object, Task>(h => h.HandleAsync)
            );
        }

        public IEventHandlerCollection Add<TEvent>(IEventHandler<TEvent> handler)
        {
            Ensure.NotNull(handler, "handler");

            HandlerDescriptor descriptor = descriptorProvider.Get(handler, typeof(TEvent));
            HashSet<HandlerDescriptor> handlers;
            if (!storage.TryGetValue(descriptor.ArgumentType, out handlers))
                storage[descriptor.ArgumentType] = handlers = new HashSet<HandlerDescriptor>();

            handlers.Add(descriptor);
            return this;
        }

        public IEventHandlerCollection Remove<TEvent>(IEventHandler<TEvent> handler)
        {
            Ensure.NotNull(handler, "handler");

            HandlerDescriptor descriptor = descriptorProvider.Get(handler, typeof(TEvent));
            HashSet<HandlerDescriptor> handlers;
            if (storage.TryGetValue(descriptor.ArgumentType, out handlers))
                handlers.Remove(descriptor);

            return this;
        }

        public Task PublishAsync<TEvent>(TEvent eventPayload)
        {
            Ensure.NotNull(eventPayload, "eventPayload");

            ArgumentDescriptor argument = descriptorProvider.Get(eventPayload.GetType());
            HashSet<HandlerDescriptor> handlers;
            if (storage.TryGetValue(argument.ArgumentType, out handlers))
                return PublishAsync(handlers, argument, eventPayload);

            return Async.CompletedTask;
        }

        private Task PublishAsync(IEnumerable<HandlerDescriptor> handlers, ArgumentDescriptor argument, object eventPayload)
        {
            bool hasContextHandler = handlers.Any(d => d.IsContext);
            bool hasEnvelopeHandler = hasContextHandler || handlers.Any(d => d.IsEnvelope);

            object payload = eventPayload;
            object context = null;
            Envelope envelope = null;

            IEvent eventWithKey = null;
            if (argument.IsContext)
            {
                // If passed argument is context, throw.
                throw Ensure.Exception.NotSupported("PersistentEventDispatcher doesn't support passing in event handler context.");
            }
            else
            {
                // If passed argument is not context, try to create it if needed.
                if (argument.IsEnvelope)
                {
                    // If passed argument is envelope, extract payload.
                    envelope = (Envelope)payload;
                    payload = envelope.Body;
                }
                else
                {
                    eventWithKey = payload as IEvent;
                    hasEnvelopeHandler = hasEnvelopeHandler || eventWithKey != null;

                    // If passed argument is not envelope, try to create it if needed.
                    if (hasEnvelopeHandler)
                    {
                        //TODO: Wrap reflection.
                        MethodInfo envelopeCreateMethod = typeof(Envelope)
                            .GetMethods(BindingFlags.Static | BindingFlags.Public)
                            .First(m => m.Name == "Create" && m.IsGenericMethod)
                            .MakeGenericMethod(argument.ArgumentType);

                        envelope = (Envelope)envelopeCreateMethod.Invoke(null, new object[] { payload });
                    }
                }

                if (hasContextHandler)
                {
                    Type contextType = typeof(DefaultEventHandlerContext<>).MakeGenericType(argument.ArgumentType);
                    context = Activator.CreateInstance(contextType, envelope, this, this);
                }
            }

            if (eventWithKey == null)
                eventWithKey = payload as IEvent;

            // TODO: If we have the envelope and delay is used, schedule the execution...
            TimeSpan delay;
            if (envelope.TryGetDelay(out delay))
            {

            }

            return Task.Factory.StartNew(async () => 
            {
                foreach (HandlerDescriptor handler in handlers)
                {
                    if (handler.IsContext)
                        await handler.Execute(context);
                    else if (handler.IsEnvelope)
                        await handler.Execute(envelope);
                    else if (handler.IsPlain)
                        await handler.Execute(eventPayload);
                    else
                        throw Ensure.Exception.UndefinedHandlerType(handler);

                    if (eventWithKey != null)
                        await eventStore.PublishedAsync(eventWithKey.Key, handler.HandlerIdentifier);
                }
            });
        }

        /// <summary>
        /// Re-publishes events from unpublished queue.
        /// Uses <paramref name="formatter"/> to deserialize events from store.
        /// </summary>
        /// <param name="formatter">The event deserializer.</param>
        /// <returns>The continuation task.</returns>
        public async Task RecoverAsync(IDeserializer formatter)
        {
            IEnumerable<EventPublishingModel> models = await eventStore.GetAsync();
            foreach (EventPublishingModel model in models)
            {
                IEvent eventModel = formatter.DeserializeEvent(Type.GetType(model.Event.EventKey.Type), model.Event.Payload);
                await RecoverEventAsync(eventModel, model.PublishedHandlerIdentifiers);
            }

            await eventStore.ClearAsync();
        }

        private async Task RecoverEventAsync(IEvent model, IEnumerable<string> handlerIdentifiers)
        {
            ArgumentDescriptor argument = descriptorProvider.Get(model.GetType());

            HashSet<HandlerDescriptor> handlers;
            if (storage.TryGetValue(argument.ArgumentType, out handlers))
            {
                IEnumerable<HandlerDescriptor> unPublishedHandlers = handlers.Where(h => !handlerIdentifiers.Contains(h.HandlerIdentifier));
                if (unPublishedHandlers.Any())
                    await PublishAsync(unPublishedHandlers, argument, model);
            }
        }
    }
}
