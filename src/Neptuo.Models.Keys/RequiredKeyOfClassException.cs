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
        /// A class of the passed in key.
        /// </summary>
        public Type UsedType { get; private set; }

        /// <summary>
        /// An enumeration of expected key classes.
        /// </summary>
        public IEnumerable<Type> ExpectedTypes { get; private set; }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public RequiredKeyOfClassException(Type usedType, params Type[] expectedTypes)
            : this(usedType, (IEnumerable<Type>)expectedTypes)
        { }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public RequiredKeyOfClassException(Type usedType, IEnumerable<Type> expectedTypes)
            : base(String.Format("Expected key '{0}', but got '{1}'.", String.Join(", ", expectedTypes.Select(t => $"'{t.Name}'")), usedType.Name))
        {
            UsedType = usedType;
            ExpectedTypes = expectedTypes;
        }

        /// <summary>
        /// Creates a new instance for deserialization.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The streaming context.</param>
        protected RequiredKeyOfClassException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
