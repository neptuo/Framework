using Neptuo.Events.Handlers;
using Neptuo.Internals;
using Neptuo.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Events
{
    /// <summary>
    /// The implementation of <see cref="IEventDispatcher"/> with notifications to the <see cref="IEventPublishObserver"/>.
    /// </summary>
    public class PersistentEventDispatcher : IEventDispatcher, IEventHandlerCollection
    {
        private readonly Dictionary<Type, HashSet<HandlerDescriptor>> storage = new Dictionary<Type, HashSet<HandlerDescriptor>>();
        private readonly IEventPublishObserver publishObserver;
        private readonly HandlerDescriptorProvider descriptorProvider;

        public PersistentEventDispatcher(IEventPublishObserver publishObserver)
        {
            Ensure.NotNull(publishObserver, "publishObserver");
            this.publishObserver = publishObserver;
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

        public async Task PublishAsync<TEvent>(TEvent eventPayload)
        {
            //TODO: Execute on different thread!
            
            ArgumentDescriptor argument = descriptorProvider.Get(typeof(TEvent));

            // 1) Find all handlers.
            HashSet<HandlerDescriptor> handlers;
            if (storage.TryGetValue(argument.ArgumentType, out handlers))
            {
                bool hasContextHandler = handlers.Any(d => d.IsContext);
                bool hasEnvelopeHandler = hasContextHandler || handlers.Any(d => d.IsEnvelope);

                object payload = eventPayload;
                object context = null;
                object envelope = null;

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
                        envelope = payload;
                        payload = ((Envelope)envelope).Body;
                    }
                    else
                    {
                        // If passed argument is not envelope, try to create it if needed.
                        if (hasEnvelopeHandler)
                            envelope = Envelope.Create(eventPayload);
                    }

                    if (hasContextHandler)
                    {
                        Type contextType = typeof(DefaultEventHandlerContext<>).MakeGenericType(argument.ArgumentType);
                        context = Activator.CreateInstance(contextType, envelope, this, this);
                    }
                }

                IEvent eventWithKey = payload as IEvent;
                foreach (HandlerDescriptor handler in handlers)
                {
                    if (handler.IsContext)
                        await handler.Execute(context);
                    else if (handler.IsEnvelope)
                        await handler.Execute(envelope);
                    else if (handler.IsPlain)
                        await handler.Execute(eventPayload);
                    else
                        throw Ensure.Exception.InvalidOperation("The handler '{0}' is of undefined type (not plain, not envelope, not context).", handler.HandlerIdentifier);

                    if (eventWithKey != null)
                        await publishObserver.OnPublishAsync(eventWithKey.Key, handler.HandlerIdentifier);
                }
            }
        }
    }
}
