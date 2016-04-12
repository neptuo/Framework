using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Events
{
    /// <summary>
    /// The observer of publishing events to the handlers.
    /// </summary>
    public interface IEventPublishObserver
    {
        /// <summary>
        /// Executed when event with <paramref name="eventKey"/> was published to the handler with <paramref name="handlerIdentifier"/>.
        /// </summary>
        /// <param name="eventKey">The published event key.</param>
        /// <param name="handlerIdentifier">The idenfier of the handler where event was published.</param>
        Task OnPublishAsync(IKey eventKey, string handlerIdentifier);
    }
}
