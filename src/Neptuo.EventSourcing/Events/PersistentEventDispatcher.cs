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

            // TODO:
            // - Store complex handler descriptor (only for those which has identifier, publishment to the observer will be executed).
            // - Determine handler identifier.
            HandlerDescriptor descriptor = descriptorProvider.Create(handler, typeof(TEvent));

            HashSet<HandlerDescriptor> handlers;
            if (!storage.TryGetValue(descriptor.ArgumentType, out handlers))
                storage[descriptor.ArgumentType] = handlers = new HashSet<HandlerDescriptor>();

            handlers.Add(descriptor);
            return this;
        }

        public IEventHandlerCollection Remove<TEvent>(IEventHandler<TEvent> handler)
        {
            Ensure.NotNull(handler, "handler");

            HandlerDescriptor descriptor = descriptorProvider.Create(handler, typeof(TEvent));
            HashSet<HandlerDescriptor> handlers;
            if (storage.TryGetValue(descriptor.ArgumentType, out handlers))
                handlers.Remove(descriptor);

            return this;
        }

        public Task PublishAsync<TEvent>(TEvent payload)
        {
            // TODO:
            // 1) Find all handlers.
            // 2) After publishing to each one, call publishObserver.

            throw new NotImplementedException();
        }
    }
}
