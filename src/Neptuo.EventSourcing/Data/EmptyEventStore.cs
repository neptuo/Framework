using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neptuo.Models.Keys;
using Neptuo.Threading.Tasks;

namespace Neptuo.Data
{
    /// <summary>
    /// The empty implementation of <see cref="IEventStore"/> and <see cref="IEventPublishingStore"/>.
    /// </summary>
    public class EmptyEventStore : IEventStore, IEventPublishingStore, IEventRebuilderStore
    {
        public Task ClearAsync()
        {
            return Async.CompletedTask;
        }

        public IEnumerable<EventModel> Get(IKey aggregateKey)
        {
            return Enumerable.Empty<EventModel>();
        }

        public IEnumerable<EventModel> Get(IKey aggregateKey, int version)
        {
            return Enumerable.Empty<EventModel>();
        }

        public Task<IEnumerable<EventPublishingModel>> GetAsync()
        {
            return Task.FromResult(Enumerable.Empty<EventPublishingModel>());
        }

        public Task<IEnumerable<EventModel>> GetAsync(IEnumerable<string> eventTypes)
        {
            return Task.FromResult(Enumerable.Empty<EventModel>());
        }

        public Task PublishedAsync(IKey eventKey, string handlerIdentifier)
        {
            return Async.CompletedTask;
        }

        public void Save(IEnumerable<EventModel> events)
        { }
    }
}
