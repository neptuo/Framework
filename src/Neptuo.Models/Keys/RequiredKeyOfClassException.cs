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
    public class RequiredKeyOfClassException : Exception
    {
        /// <summary>
        /// The type of the passed in key.
        /// </summary>
        public Type UsedType { get; private set; }

        /// <summary>
        /// The type of the expected key.
        /// </summary>
        public Type ExpectedType { get; private set; }
        
        /// <summary>
        /// Creates new empty instance.
        /// </summary>
        public RequiredKeyOfClassException(Type usedType, Type expectedType)
            : base(String.Format("Expected key '{0}', but got '{1}'.", usedType.Name, expectedType.Name))
        {
            UsedType = usedType;
            ExpectedType = expectedType;
        }

        /// <summary>
        /// Creates new instance for deserialization.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The streaming context.</param>
        protected RequiredKeyOfClassException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
