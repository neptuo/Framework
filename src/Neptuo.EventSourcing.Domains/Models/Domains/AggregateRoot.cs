using Neptuo.Events;
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
    /// The base class for aggregate root model.
    /// </summary>
    public class AggregateRoot : IAggregateRoot
    {
        private static readonly AggregateRootHandlerCollection handlers = new AggregateRootHandlerCollection();
        private readonly List<IEvent> events = new List<IEvent>();

        /// <summary>
        /// The aggregate root unique key.
        /// </summary>
        public IKey Key { get; private set; }

        /// <summary>
        /// The current version of the aggregate root.
        /// </summary>
        public int Version { get; private set; }

        /// <summary>
        /// The enumeration of unpublished events.
        /// </summary>
        public IEnumerable<IEvent> Events
        {
            get { return events; }
        }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        public AggregateRoot()
        {
            EnsureHandlerRegistration();
            Key = KeyFactory.Create(GetType());
        }

        /// <summary>
        /// Loads instance with <paramref name="key"/>.
        /// </summary>
        /// <param name="key">The key of this instance.</param>
        /// <param name="events">The enumeration of events describing current state.</param>
        public AggregateRoot(IKey key, IEnumerable<IEvent> events)
        {
            Ensure.Condition.NotEmptyKey(key, "key");
            Ensure.Condition.NotDifferentKeyType(key, GetType().Name, "key");
            Ensure.NotNull(events, "events");
            EnsureHandlerRegistration();
            Key = key;

            foreach (IEvent payload in events)
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
        protected void Publish(IEvent payload)
        {
            Event supportedPayload = payload as Event;
            if (supportedPayload != null)
            {
                supportedPayload.AggregateKey = Key;
                supportedPayload.Version = ++Version;
            }

            handlers.Publish(this, payload);
            events.Add(payload);
        }

        /// <summary>
        /// Updates aggregate root inner state.
        /// Any raised exceptions are wrapped in <see cref="AggregateRootException"/>.
        /// </summary>
        /// <param name="handler"></param>
        protected Task UpdateState(Action handler)
        {
            try
            {
                handler();
            }
            catch (Exception e)
            {
                if (e is AggregateRootException)
                    throw e;
                else
                    throw new AggregateRootException("The error raised during updating state.", e);
            }

            return Task.FromResult(true);
        }
    }
}
