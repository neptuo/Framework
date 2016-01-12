using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Metadata
{
    /// <summary>
    /// Raised when properties are defined to specific version, but no constructor.
    /// </summary>
    public class MissingVersionConstructorException : CompositeMetadataException
    {
        /// <summary>
        /// The type where the constructor is missing.
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// The version for which the constrcutor is missing.
        /// </summary>
        public int Version { get; private set; }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="type">The type where the constructor is missing.</param>
        /// <param name="version">The version for which the constrcutor is missing.</param>
        public MissingVersionConstructorException(Type type, int version)
            : base(String.Format("Missing constructor in type '{0}' for version '{1}'.", type.FullName, version))
        {
            Type = type;
            Version = version;
        }
    }
}
