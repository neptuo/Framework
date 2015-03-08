using Neptuo.Activators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Pipelines.Events.Handlers
{
    /// <summary>
    /// Wrapper for <typeparamref name="TEventHandler"/> with resolve using <see cref="IDependencyProvider"/> for each event handling.
    /// </summary>
    /// <typeparam name="TEventHandler">Type of inner handler to resolve and use.</typeparam>
    /// <typeparam name="TEvent">Type of event data.</typeparam>
    public class ActivatorEventHandler<TEventHandler, TEvent> : IEventHandler<TEvent>
        where TEventHandler : IEventHandler<TEvent>
    {
        private readonly IActivator<TEventHandler> innerHandlerFactory;

        /// <summary>
        /// Creates new instance and uses <paramref name="innerHandlerFactory"/> 
        /// to resolve inner handler of type <typeparamref name="TEventHandler"/>.
        /// </summary>
        /// <param name="innerHandlerFactory">Instance provider for inner handler of type <typeparamref name="TEventHandler"/>.</param>
        public ActivatorEventHandler(IActivator<TEventHandler> innerHandlerFactory)
        {
            Ensure.NotNull(innerHandlerFactory, "innerHandlerFactory");
            this.innerHandlerFactory = innerHandlerFactory;
        }

        public Task HandleAsync(TEvent payload)
        {
            TEventHandler innerHandler = innerHandlerFactory.Create();
            return innerHandler.HandleAsync(payload);
        }
    }
}
