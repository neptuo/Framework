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
    }
}
