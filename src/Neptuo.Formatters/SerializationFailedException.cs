using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters
{
    /// <summary>
    /// Raised when serialization of the input failed.
    /// </summary>
    public class SerializationFailedException : FormatterException
    {
        /// <summary>
        /// Gets a type of the input.
        /// </summary>
        public Type InputType { get; private set; }

        /// <summary>
        /// Creates a new instance for failed serialization of <paramref name="inputType"/>.
        /// </summary>
        /// <param name="inputType">The type of the input.</param>
        public SerializationFailedException(Type inputType)
            : base(String.Format("Serialization of the '{0}' failed.", inputType.AssemblyQualifiedName))
        {
            InputType = inputType;
        }

        /// <summary>
        /// Creates a new instance for failed serialization of <paramref name="inputType"/>.
        /// </summary>
        /// <param name="inputType">The type of the input.</param>
        /// <param name="inner">An exception cause.</param>
        public SerializationFailedException(Type inputType, Exception inner)
            : base(String.Format("Serialization of the '{0}' failed.", inputType.AssemblyQualifiedName))
        {
            InputType = inputType;
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="info">A serialization info.</param>
        /// <param name="context">A streaming context.</param>
        protected SerializationFailedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
