using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Generics
{
    /// <summary>
    /// Raised when serialization of the input failed.
    /// </summary>
    public class SerializationFailedException : FormatterException
    {
        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public SerializationFailedException()
            : base("Serialization failed.")
        { }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="inner">An exception cause.</param>
        public SerializationFailedException(Exception inner)
            : base("Serialization failed.", inner)
        { }

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
