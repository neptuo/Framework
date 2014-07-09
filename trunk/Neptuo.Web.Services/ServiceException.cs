using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services
{
    /// <summary>
    /// Base exception for Neptuo.Web.Services.
    /// </summary>
    [Serializable]
    public class ServiceException : Exception
    {
        /// <summary>
        /// Creates new instance.
        /// </summary>
        public ServiceException() 
        { }

        /// <summary>
        /// Creates new instance with <paramref name="message"/>.
        /// </summary>
        /// <param name="message">Exception description.</param>
        public ServiceException(string message) 
            : base(message) 
        { }

        /// <summary>
        /// Creates new instance from <paramref name="inner"/> with <paramref name="message"/>.
        /// </summary>
        /// <param name="message">Exception description.</param>
        /// <param name="inner">Exception that caused this exception.</param>
        public ServiceException(string message, Exception inner) 
            : base(message, inner) 
        { }

        /// <summary>
        /// Creates new instance for deserialization.
        /// </summary>
        /// <param name="info">Serialization info.</param>
        /// <param name="context">Streaming context.</param>
        protected ServiceException(SerializationInfo info, StreamingContext context)
            : base(info, context) 
        { }
    }
}
