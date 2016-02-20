using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Events
{
    /// <summary>
    /// Base implementation of <see cref="IEvent"/> with support for automatic fill of parameters from <see cref="AggregateRoot"/>.
    /// </summary>
    public abstract class Event : IEvent
    {
        public IKey Key { get; private set; }
        public IKey AggregateKey { get; internal set; }
        public int Version { get; internal set; }

        /// <summary>
        /// Creates new empty instance.
        /// </summary>
        protected Event()
        {
            Key = GuidKey.Create(Guid.NewGuid(), GetType().Name);
        }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="aggreagateKey">The key of the aggregate where originated.</param>
        /// <param name="version">The version of the aggregate.</param>
        protected Event(IKey aggreagateKey, int version)
            : this()
        {
            Ensure.Condition.NotEmptyKey(aggreagateKey, "aggreagateKey");
            AggregateKey = aggreagateKey;
            Version = version;
        }
    }
}
