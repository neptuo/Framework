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
        /// An enumeration of expected types.
        /// </summary>
        public IEnumerable<string> ExpectedTypes { get; private set; }
        
        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public RequiredKeyOfTypeException(string usedType, params string[] expectedTypes)
            : this(usedType, (IEnumerable<string>)expectedTypes)
        { }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public RequiredKeyOfTypeException(string usedType, IEnumerable<string> expectedTypes)
            : base(String.Format("Expected key {0}, but got '{1}'.", String.Join(", ", expectedTypes.Select(t => $"'{t}'")), usedType))
        {
            UsedType = usedType;
            ExpectedTypes = expectedTypes;
        }

        /// <summary>
        /// Creates a new instance for deserialization.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The streaming context.</param>
        protected RequiredKeyOfTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
