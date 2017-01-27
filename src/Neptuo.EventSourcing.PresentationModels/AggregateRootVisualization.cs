using Neptuo;
using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    /// <summary>
    /// A model for describing an aggregate root visualization.
    /// </summary>
    public class AggregateRootVisualization
    {
        /// <summary>
        /// Gets a key of the aggregate root.
        /// </summary>
        public IKey Key { get; private set; }

        /// <summary>
        /// Gets a model definition of the aggregate root.
        /// </summary>
        public IModelDefinition Definition { get; private set; }

        /// <summary>
        /// Gets a list of the events visualization applied to the aggregate root.
        /// </summary>
        public IReadOnlyList<ObjectVisualization> Events { get; private set; }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="key">A key of the aggregate root.</param>
        /// <param name="definition">A model definition of the aggregate root.</param>
        /// <param name="events">A list of the events visualization applied to the aggregate root.</param>
        public AggregateRootVisualization(IKey key, IModelDefinition definition, IReadOnlyList<ObjectVisualization> events)
        {
            Ensure.Condition.NotEmptyKey(key);
            Ensure.NotNull(definition, "definition");
            Ensure.NotNull(events, "events");
            Key = key;
            Definition = definition;
            Events = events;
        }
    }
}
