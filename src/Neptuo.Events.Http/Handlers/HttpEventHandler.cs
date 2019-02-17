using Neptuo.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Events.Handlers
{
    /// <summary>
    /// An implementation of <see cref="IEventHandler{TEvent}"/> that transfers events over HTTP.
    /// </summary>
    /// <typeparam name="TEvent">A type of the event.</typeparam>
    public class HttpEventHandler<TEvent> : IEventHandler<TEvent>
    {
        private readonly HttpEventDispatcher dispatcher;

        /// <summary>
        /// Creates a new instance that sends events of type <typeparamref name="TEvent"/> throught <paramref name="objectSender"/>.
        /// </summary>
        /// <param name="objectSender">An object sender.</param>
        public HttpEventHandler(ObjectSender objectSender) => dispatcher = new HttpEventDispatcher(objectSender);

        public Task HandleAsync(TEvent payload) => dispatcher.PublishAsync(payload);
    }
}
