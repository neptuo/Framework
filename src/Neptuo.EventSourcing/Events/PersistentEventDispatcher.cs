using Neptuo.Components;
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

        public Task PublishAsync<TEvent>(TEvent payload)
        {
            ArgumentDescriptor argument = descriptorProvider.Get(typeof(TEvent));

            // 1) Find all handlers.
            HashSet<HandlerDescriptor> handlers;
            if (storage.TryGetValue(argument.ArgumentType, out handlers))
            {
                bool hasContextHandler = handlers.Any(d => d.IsContext);
                bool hasEnvelopeHandler = hasContextHandler || handlers.Any(d => d.IsEnvelope);

                object context = null;
                object envelope = null;

                if (argument.IsContext)
                {
                    // If passed argument is context, extract envelope and payload.
                    context = payload;
                    envelope = default(TEvent); //TODO: Extract envelope from context.
                    payload = default(TEvent); //TODO: Extract payload from context.
                }
                else
                {
                    // If passed argument is not context, try to create it if needed.
                    if (argument.IsEnvelope)
                    {
                        // If passed argument is envelope, extract payload.
                        envelope = payload;
                        payload = default(TEvent); //TODO: Extract payload from envelope.
                    }
                    else
                    {
                        // If passed argument is not envelope, try to create it if needed.
                        if (hasEnvelopeHandler)
                            envelope = Envelope.Create(payload);
                    }

                    if (hasContextHandler)
                        context = default(TEvent); // TODO: Create instance of the context.
                }

                // TODO: Publish to handlers in the order of the registration (? May be it is not important).
                // 2) After publishing to each one, call publishObserver.
                // - Store complex handler descriptor (only for those which has identifier, publishment to the observer will be executed).
                throw new NotImplementedException();
            }

            return Task.FromResult(true);
        }
    }
}
