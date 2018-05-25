using Neptuo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Keys
{
    /// <summary>
    /// An exception raised when trying to serialize key to parameters and there is missing handler for key class.
    /// </summary>
    [Serializable]
    public class MissingKeyClassToParametersMappingException : Exception
    {
        /// <summary>
        /// Gets a class of the key.
        /// </summary>
        public Type KeyType { get; private set; }

        /// <summary>
        /// Creates a instance.
        /// </summary>
        /// <param name="keyType">A class of the key.</param>
        public MissingKeyClassToParametersMappingException(Type keyType)
        {
            Ensure.NotNull(keyType, "keyType");
            KeyType = keyType;
        }

        /// <summary>
        /// Creates new instance for deserialization.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The streaming context.</param>
        protected MissingKeyClassToParametersMappingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
