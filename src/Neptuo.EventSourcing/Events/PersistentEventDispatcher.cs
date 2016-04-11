using Neptuo.Events.Handlers;
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
        private readonly IEventPublishObserver publishObserver;

        public PersistentEventDispatcher(IEventPublishObserver publishObserver)
        {
            Ensure.NotNull(publishObserver, "publishObserver");
            this.publishObserver = publishObserver;
        }

        public IEventHandlerCollection Add<TEvent>(IEventHandler<TEvent> handler)
        {
            Ensure.NotNull(handler, "handler");

            // TODO:
            // - Store complex handler descriptor (only for those which has identifier, publishment to the observer will be executed).
            // - Determine handler identifier.
            // - Build lambda function used to publish event to the handler.
            // - Store information about Envelope or EventHandlerContext requirement.

            throw new NotImplementedException();
        }

        public IEventHandlerCollection Remove<TEvent>(IEventHandler<TEvent> handler)
        {
            Ensure.NotNull(handler, "handler");
            throw new NotImplementedException();
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
