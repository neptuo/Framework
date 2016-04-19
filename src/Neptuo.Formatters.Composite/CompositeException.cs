using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters
{
    /// <summary>
    /// The base for composite exceptions.
    /// </summary>
    [Serializable]
    public class CompositeException : FormatterException
    {
        /// <summary>
        /// Creates new empty instance.
        /// </summary>
        public CompositeException()
        { }

        /// <summary>
        /// Creates new instance with the <paramref name="message"/>.
        /// </summary>
        /// <param name="message">The text description of the problem.</param>
        public CompositeException(string message)
            : base(message)
        { }

        /// <summary>
        /// Creates new instance with the <paramref name="message"/> and <paramref name="inner"/> exception.
        /// </summary>
        /// <param name="message">The text description of the problem.</param>
        /// <param name="inner">The inner cause of the exceptional state.</param>
        public CompositeException(string message, Exception inner)
            : base(message, inner)
        { }

        /// <summary>
        /// Creates new instance for deserialization.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The streaming context.</param>
        protected CompositeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
