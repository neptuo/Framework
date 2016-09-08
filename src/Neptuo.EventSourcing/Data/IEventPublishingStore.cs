using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data
{
    /// <summary>
    /// The underlying store for the persistent event delivery.
    /// </summary>
    public interface IEventPublishingStore : IEventStore
    {
        /// <summary>
        /// Returns the enumeration of the unpublished events which were published to the handlers.
        /// </summary>
        /// <returns>The enumeration of the unpublished events which were published to the handlers.</returns>
        Task<IEnumerable<EventPublishingModel>> GetAsync();

        /// <summary>
        /// Saves information about publishing <paramref name="eventKey"/> to the <paramref name="handlerIdentifier"/>.
        /// </summary>
        /// <param name="eventKey">The key of the published event.</param>
        /// <param name="handlerIdentifier">The identifier of the handler where the event was published to.</param>
        /// <returns>The continuation task.</returns>
        Task PublishedAsync(IKey eventKey, string handlerIdentifier);

        /// <summary>
        /// Clears the queue of the unpublished events.
        /// </summary>
        /// <returns>The continuation task.</returns>
        Task ClearAsync();
    }
}
