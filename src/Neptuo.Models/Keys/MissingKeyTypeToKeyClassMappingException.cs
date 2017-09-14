using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Keys
{
    /// <summary>
    /// An exception raised when using <see cref="IKeyToParametersConverter"/> to get <see cref="IKey"/> and mapping between <see cref="IKey.Type"/> and concrete implementation is missing.
    /// </summary>
    [Serializable]
    public class MissingKeyTypeToKeyClassMappingException : Exception
    {
        /// <summary>
        /// Get a key type with missing mapping to <see cref="Type"/>.
        /// </summary>
        public string KeyType { get; private set; }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="keyType">A key type with missing mapping to <see cref="Type"/>.</param>
        public MissingKeyTypeToKeyClassMappingException(string keyType)
        {
            KeyType = keyType;
        }

        /// <summary>
        /// Creates new instance for deserialization.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The streaming context.</param>
        protected MissingKeyTypeToKeyClassMappingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
