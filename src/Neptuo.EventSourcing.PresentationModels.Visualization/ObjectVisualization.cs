using Neptuo;
using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    /// <summary>
    /// A model for describing a single item for visualization.
    /// </summary>
    public class ObjectVisualization
    {
        /// <summary>
        /// Gets a model definition of the item.
        /// </summary>
        public IModelDefinition Definition { get; private set; }

        /// <summary>
        /// Gets a value getter of the time.
        /// </summary>
        public IModelValueGetter Getter { get; private set; }

        /// <summary>
        /// Gets a collection of metadata from an envelope.
        /// </summary>
        public IReadOnlyKeyValueCollection Metadata { get; private set; }

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="definition">A model definition of the item.</param>
        /// <param name="getter">A value getter of the time.</param>
        /// <param name="metadata">A collection of metadata from an envelope</param>
        public ObjectVisualization(IModelDefinition definition, IModelValueGetter getter, IReadOnlyKeyValueCollection metadata)
        {
            Ensure.NotNull(definition, "definition");
            Ensure.NotNull(getter, "getter");
            Ensure.NotNull(metadata, "metadata");
            Definition = definition;
            Getter = getter;
            Metadata = metadata;
        }
    }
}
