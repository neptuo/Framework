using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Metadata
{
    /// <summary>
    /// A single version of composite type.
    /// Defines a version number, a constructor and a set of properties in this version.
    /// </summary>
    public class CompositeVersion
    {
        /// <summary>
        /// Gets a version of the model definition.
        /// </summary>
        public int Version { get; private set; }

        /// <summary>
        /// Gets a constructor for this version.
        /// </summary>
        public CompositeConstructor Constructor { get; private set; }

        /// <summary>
        /// Gets a set of properties for this version.
        /// </summary>
        public IEnumerable<CompositeProperty> Properties { get; private set; }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="version">A version of the model definition.</param>
        /// <param name="constructor">A composite constructor.</param>
        /// <param name="properties">A set of properties.</param>
        public CompositeVersion(int version, CompositeConstructor constructor, IEnumerable<CompositeProperty> properties)
        {
            Ensure.Positive(version, "version");
            Ensure.NotNull(constructor, "constructor");
            Ensure.NotNull(properties, "properties");
            Version = version;
            Constructor = constructor;
            Properties = properties;
        }
    }
}
