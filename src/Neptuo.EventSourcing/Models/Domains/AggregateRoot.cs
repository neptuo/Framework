using Neptuo.Events.Handlers;
using Neptuo.Linq.Expressions;
using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Domains
{
    /// <summary>
    /// Base for aggregate root model.
    /// </summary>
    public class AggregateRoot : IDomainModel<StringKey>
    {
        private static readonly AggregateRootHandlerCollection handlers = new AggregateRootHandlerCollection();
        private readonly List<object> events = new List<object>();

        /// <summary>
        /// Aggregate root unique key.
        /// </summary>
        public StringKey Key { get; private set; }

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
            EnsureHandlerRegistration();
            Key = StringKey.Create(Guid.NewGuid().ToString(), GetType().Name);
        }

        /// <summary>
        /// Creates new instance with <paramref name="key"/>.
        /// </summary>
        /// <param name="key">The key of this instance.</param>
        /// <param name="events">The enumeration of events describing current state.</param>
        public AggregateRoot(StringKey key, IEnumerable<object> events)
        {
            Ensure.Condition.NotEmptyKey(key, "key");
            Ensure.Condition.SameKeyType(key, GetType().Name, "key");
            Ensure.NotNull(events, "events");
            EnsureHandlerRegistration();
            Key = key;

            foreach (object payload in events)
                handlers.Publish(this, payload);
        }

        /// <summary>
        /// Ensures registration of event handlers for current type from implementations of <see cref="IEventHandler{T}"/>.
        /// </summary>
        private void EnsureHandlerRegistration()
        {
            Type type = GetType();
            if (!handlers.Has(type))
                handlers.Map(type);
        }

        /// <summary>
        /// Stores <paramref name="payload"/> and executes handler for state modification.
        /// </summary>
        /// <param name="payload">The event payload to publish.</param>
        protected void Publish(object payload)
        {
            handlers.Publish(this, payload);
            events.Add(payload);
        }
    }
}
