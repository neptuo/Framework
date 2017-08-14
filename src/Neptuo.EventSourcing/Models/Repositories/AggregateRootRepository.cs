using Neptuo.Activators;
using Neptuo.Auditing;
using Neptuo.Data;
using Neptuo.Events;
using Neptuo.Formatters;
using Neptuo.Internals;
using Neptuo.Models.Domains;
using Neptuo.Models.Keys;
using Neptuo.Models.Snapshots;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Repositories
{
    /// <summary>
    /// The implementation of EventSourcing AggregateRoot repository.
    /// </summary>
    /// <typeparam name="T">The type of the aggregate root.</typeparam>
    public class AggregateRootRepository<T> : IRepository<T, IKey>
        where T : AggregateRoot
    {
        private readonly IEventStore store;
        private readonly IFormatter formatter;
        private readonly IAggregateRootFactory<T> factory;
        private readonly IEventDispatcher eventDispatcher;
        private ISnapshotProvider snapshotProvider;
        private ISnapshotStore snapshotStore;
        private IEnvelopeDecorator auditor;

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="store">The underlaying event store.</param>
        /// <param name="formatter">The formatter for serializing and deserializing event payloads.</param>
        /// <param name="factory">The aggregate root factory.</param>
        /// <param name="eventDispatcher">The dispatcher for newly created events in the aggregates.</param>
        public AggregateRootRepository(IEventStore store, IFormatter formatter, IAggregateRootFactory<T> factory, IEventDispatcher eventDispatcher)
        {
            Ensure.NotNull(store, "store");
            Ensure.NotNull(formatter, "formatter");
            Ensure.NotNull(factory, "factory");
            Ensure.NotNull(eventDispatcher, "eventDispatcher");
            this.store = store;
            this.formatter = formatter;
            this.factory = factory;
            this.eventDispatcher = eventDispatcher;
        }

        /// <summary>
        /// Integrates snapshots into repository.
        /// </summary>
        /// <param name="snapshotProvider">The snapshot provider.</param>
        /// <param name="snapshotStore">The store for snapshots.</param>
        /// <returns>Self (for fluency).</returns>
        public AggregateRootRepository<T> UseSnapshots(ISnapshotProvider snapshotProvider, ISnapshotStore snapshotStore)
        {
            Ensure.NotNull(snapshotProvider, "snapshotProvider");
            Ensure.NotNull(snapshotStore, "snapshotStore");
            this.snapshotProvider = snapshotProvider;
            this.snapshotStore = snapshotStore;
            return this;
        }

        /// <summary>
        /// Returns <c>true</c> if snapshots are enabled.
        /// </summary>
        /// <returns><c>true</c> if snapshots are enabled; otherwise <c>false</c>.</returns>
        public bool HasSnapshots()
        {
            return snapshotProvider != null && snapshotStore != null;
        }

        /// <summary>
        /// Integrates <paramref name="auditor"/> into repository.
        /// </summary>
        /// <param name="auditor">An envelope auditor.</param>
        /// <returns></returns>
        /// <returns>Self (for fluency).</returns>
        public AggregateRootRepository<T> UseAuditing(IEnvelopeDecorator auditor)
        {
            Ensure.NotNull(auditor, "auditor");
            this.auditor = auditor;
            return this;
        }

        /// <summary>
        /// Returns <c>true</c> if auditing is enabled.
        /// </summary>
        /// <returns><c>true</c> if auditing is enabled; otherwise <c>false</c>.</returns>
        public bool HasAutiding()
        {
            return auditor != null;
        }

        public virtual void Save(T model)
        {
            Ensure.NotNull(model, "model");

            IEnumerable<IEvent> events = model.Events;
            if (events.Any())
            {
                // Serialize and save all new events.
                IEnumerable<EventModel> eventModels = events.Select(e =>
                {
                    string payload = null;
                    if (auditor != null)
                    {
                        Envelope<IEvent> envelope = Envelope.Create(e);
                        auditor.Decorate(envelope);

                        if (envelope.Metadata.Keys.Any())
                            payload = formatter.SerializeEvent(envelope);
                    }

                    if (payload == null)
                        payload = formatter.SerializeEvent(e);

                    return new EventModel(
                        e.AggregateKey,
                        e.Key,
                        payload,
                        e.Version
                    );
                });
                store.Save(eventModels);

                // Try to create snapshot.
                if (HasSnapshots())
                {
                    ISnapshot snapshot;
                    if (snapshotProvider.TryCreate(model, out snapshot))
                        snapshotStore.Save(snapshot);
                }

                // Publish new events.
                foreach (IEvent e in events)
                    eventDispatcher.PublishAsync(e).Wait();
            }
        }

        public T Find(IKey key)
        {
            Ensure.Condition.NotEmptyKey(key);

            // Try to find snapshot.
            ISnapshot snapshot = null;
            if (HasSnapshots())
                snapshot = snapshotStore.Find(key);

            // If snapshot exists, load only newer events; otherwise load all of them.
            IEnumerable<EventModel> eventModels = null;
            if (snapshot == null)
                eventModels = store.Get(key);
            else
                eventModels = store.Get(key, snapshot.Version);

            IEnumerable<object> events = eventModels.Select(e => formatter.DeserializeEvent(Type.GetType(e.EventKey.Type), e.Payload));

            // If snapshot exists, create instance with it and newer events; otherwise create instance using all events.
            T instance = null;
            if (snapshot == null)
                instance = factory.Create(key, events);
            else
                instance = factory.Create(key, snapshot, events);

            // Return the aggregate.
            return instance;
        }
    }
}
