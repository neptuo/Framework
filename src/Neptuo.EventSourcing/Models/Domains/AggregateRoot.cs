using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Domains
{
    /// <summary>
    /// Base for aggregate root model.
    /// </summary>
    public class AggregateRoot : IDomainModel<GuidKey>
    {
        private readonly List<object> events = new List<object>();

        /// <summary>
        /// Aggregate root unique key.
        /// </summary>
        public GuidKey Key { get; private set; }

        /// <summary>
        /// Enumeration of unsaved events.
        /// </summary>
        public IEnumerable<object> Events
        {
            get { return events; }
        }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        public AggregateRoot()
        {
            Key = GuidKey.Create(Guid.NewGuid(), GetType().Name);
        }

        /// <summary>
        /// Creates new instance with <paramref name="key"/>.
        /// </summary>
        /// <param name="key">The key of this instance.</param>
        /// <param name="events">The enumeration of events describing current state.</param>
        public AggregateRoot(GuidKey key, IEnumerable<object> events)
        {
            Ensure.Condition.NotEmpty(key, "key");
            Ensure.NotNull(events, "events");

            if (key.Type != GetType().Name)
                throw Ensure.Exception.ArgumentOutOfRange("key", "Passed key of different aggregate type '{0}' to the '{1}'.", key.Type, GetType().Name);

            Key = key;

            //TODO: Replay events.
        }
    }
}
