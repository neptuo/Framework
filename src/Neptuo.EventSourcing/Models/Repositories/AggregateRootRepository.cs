using Neptuo.Activators;
using Neptuo.Models.Domains;
using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Repositories
{
    public class AggregateRootRepository<T> : IRepository<T, GuidKey>
        where T : AggregateRoot
    {
        private readonly IEventStore store;
        private readonly IEventSerializer serializer;
        private readonly IAggregateRootFactory<T> factory;

        public AggregateRootRepository(IEventStore store, IEventSerializer serializer, IAggregateRootFactory<T> factory)
        {
            Ensure.NotNull(store, "store");
            Ensure.NotNull(serializer, "serializer");
            Ensure.NotNull(factory, "factory");
            this.store = store;
            this.serializer = serializer;
            this.factory = factory;
        }

        public void Save(T model)
        {
            Ensure.NotNull(model, "model");

            IEnumerable<object> events = model.Events;
            if (events.Any())
            {
                IEnumerable<EventModel> eventModels = events.Select(e => new EventModel(model.Key, serializer.Serialize(e)));
                store.Save(eventModels);
            }
        }

        public T Find(GuidKey key)
        {
            Ensure.Condition.NotEmpty(key, "key");

            IEnumerable<EventModel> eventModels = store.Get(key);
            IEnumerable<object> events = eventModels.Select(e => serializer.Deserialize(e.Payload));
            
            T instance = factory.Create(key, events);
            return instance;
        }
    }
}
