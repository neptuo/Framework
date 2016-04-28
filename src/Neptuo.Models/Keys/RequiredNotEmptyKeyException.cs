using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Keys
{
    /// <summary>
    /// The exception used when got empty key, but required not empty.
    /// </summary>
    public class RequiredNotEmptyKeyException : Exception
    {
        public RequiredNotEmptyKeyException(string keyType)
            : base(String.Format("Required not empty key, but got empty key with type '{0}'.", keyType))
        { }

        /// <summary>
        /// Creates new instance for deserialization.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The streaming context.</param>
        protected RequiredNotEmptyKeyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
