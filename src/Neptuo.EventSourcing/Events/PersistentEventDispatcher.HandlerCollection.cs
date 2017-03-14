using Neptuo.Events.Handlers;
using Neptuo.Internals;
using Neptuo.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Events
{
    partial class PersistentEventDispatcher
    {
        internal class HandlerCollection : IEventHandlerCollection
        {
            private readonly ILog log;

            private readonly Dictionary<Type, HashSet<HandlerDescriptor>> storage;
            private readonly HandlerDescriptorProvider descriptorProvider;

            public HandlerCollection(ILogFactory logFactory, Dictionary<Type, HashSet<HandlerDescriptor>> storage, HandlerDescriptorProvider descriptorProvider)
            {
                Ensure.NotNull(logFactory, "logFactory");
                Ensure.NotNull(storage, "storage");
                Ensure.NotNull(descriptorProvider, "descriptorProvider");
                this.log = logFactory.Scope("Handlers");
                this.storage = storage;
                this.descriptorProvider = descriptorProvider;
            }

            public IEventHandlerCollection Add<TEvent>(IEventHandler<TEvent> handler)
            {
                Ensure.NotNull(handler, "handler");

                HandlerDescriptor descriptor = descriptorProvider.Get(handler, typeof(TEvent));
                HashSet<HandlerDescriptor> handlers;
                if (!storage.TryGetValue(descriptor.ArgumentType, out handlers))
                    storage[descriptor.ArgumentType] = handlers = new HashSet<HandlerDescriptor>();

                handlers.Add(descriptor);

                if (log.IsDebugEnabled())
                    log.Debug($"Added a handler '{descriptor}'.");

                return this;
            }

            public IEventHandlerCollection Remove<TEvent>(IEventHandler<TEvent> handler)
            {
                Ensure.NotNull(handler, "handler");

                HandlerDescriptor descriptor = descriptorProvider.Get(handler, typeof(TEvent));
                HashSet<HandlerDescriptor> handlers;
                if (storage.TryGetValue(descriptor.ArgumentType, out handlers))
                {
                    handlers.Remove(descriptor);

                    if (log.IsDebugEnabled())
                        log.Debug($"Removed a handler '{descriptor}'.");
                }
                else
                {
                    if (log.IsDebugEnabled())
                        log.Debug($"A handler '{handler.GetType().FullName}' not found.");
                }

                return this;
            }

        }

    }
}
