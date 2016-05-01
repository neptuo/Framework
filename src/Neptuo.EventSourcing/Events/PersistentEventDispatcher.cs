using Neptuo.Data;
using Neptuo.Events.Handlers;
using Neptuo.Exceptions;
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
using System.Threading;
using System.Threading.Tasks;

namespace Neptuo.Events
{
    /// <summary>
    /// The implementation of <see cref="IEventDispatcher"/> and <see cref="IEventHandlerCollection"/> with persistent delivery.
    /// </summary>
    public partial class PersistentEventDispatcher : IEventDispatcher
    {
        private readonly object timersLock = new object();

        private readonly Dictionary<Type, HashSet<HandlerDescriptor>> storage = new Dictionary<Type, HashSet<HandlerDescriptor>>();
        private readonly IEventPublishingStore eventStore;
        private readonly HandlerDescriptorProvider descriptorProvider;
        private readonly List<Tuple<Timer, ScheduleEventContext>> timers = new List<Tuple<Timer, ScheduleEventContext>>();

        /// <summary>
        /// The collection of event handlers.
        /// </summary>
        public IEventHandlerCollection Handlers { get; private set; }

        /// <summary>
        /// The collection of exception handlers for exceptions from the event processing.
        /// </summary>
        public IExceptionHandlerCollection EventExceptionHandlers { get; private set; }

        /// <summary>
        /// The collection of exception handlers for exceptions from the infrastructure.
        /// </summary>
        public IExceptionHandlerCollection DispatcherExceptionHandlers { get; private set; }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="store">The publishing store for command persistent delivery.</param>
        public PersistentEventDispatcher(IEventPublishingStore store)
        {
            Ensure.NotNull(store, "store");
            this.eventStore = store;

            EventExceptionHandlers = new DefaultExceptionHandlerCollection();
            DispatcherExceptionHandlers = new DefaultExceptionHandlerCollection();

            this.descriptorProvider = new HandlerDescriptorProvider(
                typeof(IEventHandler<>),
                typeof(IEventHandlerContext<>),
                TypeHelper.MethodName<IEventHandler<object>, object, Task>(h => h.HandleAsync),
                EventExceptionHandlers,
                DispatcherExceptionHandlers
            );

            Handlers = new HandlerCollection(storage, descriptorProvider);
        }

        public Task PublishAsync<TEvent>(TEvent payload)
        {
            Ensure.NotNull(payload, "payload");

            ArgumentDescriptor argument = descriptorProvider.Get(payload);
            HashSet<HandlerDescriptor> handlers;
            if (storage.TryGetValue(argument.ArgumentType, out handlers))
                return PublishAsync(handlers, argument, payload, true);

            return Async.CompletedTask;
        }

        private Task PublishAsync(IEnumerable<HandlerDescriptor> handlers, ArgumentDescriptor argument, object eventPayload, bool isEnvelopeDelayUsed)
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
                        envelope = EnvelopeFactory.Create(payload, argument.ArgumentType);
                }

                if (hasContextHandler)
                {
                    Type contextType = typeof(DefaultEventHandlerContext<>).MakeGenericType(argument.ArgumentType);
                    context = Activator.CreateInstance(contextType, envelope, this, this);
                }
            }

            if (eventWithKey == null)
                eventWithKey = payload as IEvent;

            DateTime executeAt;
            if (isEnvelopeDelayUsed && envelope.TryGetExecuteAt(out executeAt))
            {
                ScheduleEventContext scheduleContext = new ScheduleEventContext(handlers, argument, payload);
                Timer timer = new Timer(
                    OnScheduledEvent,
                    scheduleContext,
                    executeAt.Subtract(DateTime.Now),
                    TimeSpan.FromMilliseconds(-1)
                );

                lock (timersLock)
                    timers.Add(new Tuple<Timer, ScheduleEventContext>(timer, scheduleContext));
                
                return Async.CompletedTask;
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
                        await handler.Execute(payload);
                    else
                        throw Ensure.Exception.UndefinedHandlerType(handler);

                    if (eventWithKey != null && handler.HandlerIdentifier != null)
                        await eventStore.PublishedAsync(eventWithKey.Key, handler.HandlerIdentifier);
                }
            });
        }

        private void OnScheduledEvent(object state)
        {
            ScheduleEventContext context = (ScheduleEventContext)state;

            lock (timersLock)
            {
                Tuple<Timer, ScheduleEventContext> item = timers.FirstOrDefault(t => t.Item2 == context);
                if (item != null)
                    timers.Remove(item);
            }

            PublishAsync(context.Handlers, context.Argument, context.Payload, false).Wait();
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
                    await PublishAsync(unPublishedHandlers, argument, model, true);
            }
        }
    }
}
