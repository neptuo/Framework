using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.TypeMapping
{
    /// <summary>
    /// An exception used when there is missing mapping in the <see cref="ITypeNameMapper"/>.
    /// </summary>
    [Serializable]
    public class MissingTypeNameMappingException : Exception
    {
        /// <summary>
        /// Gets a type name which failed to map to <see cref="System.Type"/>.
        /// </summary>
        public string TypeName { get; private set; }

        /// <summary>
        /// Gets a type which failed to map to <see cref="string"/>.
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// Creates a new instance for missing mapping for <paramref name="typeName"/> to <see cref="Type"/>.
        /// </summary>
        public MissingTypeNameMappingException(string typeName)
            : base(string.Format("Missing mapping for '{0}' to 'Type'.", typeName))
        {
            TypeName = typeName;
        }

        /// <summary>
        /// Creates a new instance for missing mapping for <paramref name="type"/> to <see cref="string"/>.
        /// </summary>
        public MissingTypeNameMappingException(Type type)
            : base(string.Format("Missing mapping for '{0}' to 'string'.", type))
        {
            Type = type;
        }

        /// <summary>
        /// Creates a new instance for deserialization.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The streaming context.</param>
        protected MissingTypeNameMappingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
