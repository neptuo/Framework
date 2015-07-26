using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Behaviors.Processing
{
    /// <summary>
    /// Exception during pipeline factory execution.
    /// </summary>
    [Serializable]
    public class PipelineFactoryException : PipelineException
    {
        /// <summary>
        /// Creates new instance.
        /// </summary>
        public PipelineFactoryException() 
        { }

        /// <summary>
        /// Creates new instance with <paramref name="message"/>.
        /// </summary>
        /// <param name="message">Exception description.</param>
        public PipelineFactoryException(string message) 
            : base(message) 
        { }

        /// <summary>
        /// Creates new instance from <paramref name="inner"/> with <paramref name="message"/>.
        /// </summary>
        /// <param name="message">Exception description.</param>
        /// <param name="inner">Exception that caused this exception.</param>
        public PipelineFactoryException(string message, Exception inner) 
            : base(message, inner) 
        { }

        /// <summary>
        /// Creates new instance for deserialization.
        /// </summary>
        /// <param name="info">Serialization info.</param>
        /// <param name="context">Streaming context.</param>
        protected PipelineFactoryException(SerializationInfo info, StreamingContext context)
            : base(info, context) 
        { }
    }
}
