using Neptuo.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Events
{
    /// <summary>
    /// An implementation of <see cref="ICommandDispatcher"/> that transfers commands over HTTP.
    /// </summary>
    public class HttpEventDispatcher : IEventDispatcher
    {
        private readonly ObjectSender objectSender;

        /// <summary>
        /// Creates a new instance which sends objects using <paramref name="objectSender"/>.
        /// </summary>
        /// <param name="objectSender">An object sender.</param>
        public HttpEventDispatcher(ObjectSender objectSender)
        {
            Ensure.NotNull(objectSender, "objectSender");
            this.objectSender = objectSender;
        }

        public Task PublishAsync<TEvent>(TEvent payload)
        {
            Ensure.NotNull(payload, "payload");
            return objectSender.SendAsync(payload, default);
        }
    }
}
