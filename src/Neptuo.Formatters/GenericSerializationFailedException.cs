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
    public class GenericSerializationFailedException : FormatterException
    {
        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public GenericSerializationFailedException()
            : base("Serialization failed.")
        { }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="inner">An exception cause.</param>
        public GenericSerializationFailedException(Exception inner)
            : base("Serialization failed.", inner)
        { }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="info">A serialization info.</param>
        /// <param name="context">A streaming context.</param>
        protected GenericSerializationFailedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
