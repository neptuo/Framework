using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Keys
{
    /// <summary>
    /// The exception used when key of different was expected.
    /// </summary>
    [Serializable]
    public class RequiredKeyOfTypeException : Exception
    {
        /// <summary>
        /// The type of the passed in key.
        /// </summary>
        public string UsedType { get; private set; }

        /// <summary>
        /// The type of the expected key.
        /// </summary>
        public string ExpectedType { get; private set; }
        
        /// <summary>
        /// Creates new empty instance.
        /// </summary>
        public RequiredKeyOfTypeException(string usedType, string expectedType)
            : base(String.Format("Expected key '{0}', but got '{1}'.", usedType, expectedType))
        {
            UsedType = usedType;
            ExpectedType = expectedType;
        }

        /// <summary>
        /// Creates new instance for deserialization.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The streaming context.</param>
        protected RequiredKeyOfTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
