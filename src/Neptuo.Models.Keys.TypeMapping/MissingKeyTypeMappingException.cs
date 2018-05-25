using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Keys
{
    /// <summary>
    /// An exception used when there is missing mapping in the <see cref="IKeyTypeProvider"/>.
    /// </summary>
    [Serializable]
    public class MissingKeyTypeMappingException : Exception
    {
        public string KeyType { get; private set; }
        public Type Type { get; private set; }

        /// <summary>
        /// Creates new instance for missing mapping for <paramref name="keyType"/> to <see cref="Type"/>.
        /// </summary>
        public MissingKeyTypeMappingException(string keyType)
            : base(string.Format("Missing mapping for '{0}' to 'Type'.", keyType))
        {
            KeyType = keyType;
        }

        /// <summary>
        /// Creates new instance for missing mapping for <paramref name="type"/> to <see cref="IKey.Type"/>.
        /// </summary>
        public MissingKeyTypeMappingException(Type type)
            : base(string.Format("Missing mapping for '{0}' to 'KeyType'.", type))
        {
            Type = type;
        }

        /// <summary>
        /// Creates new instance for deserialization.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The streaming context.</param>
        protected MissingKeyTypeMappingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
