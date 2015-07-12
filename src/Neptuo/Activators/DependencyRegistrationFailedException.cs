using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators
{
    /// <summary>
    /// Exception that should by thrown by all implementations of <see cref="IDependencyDefinitionCollection"/> when registration is not possible.
    /// </summary>
    [Serializable]
    public class DependencyRegistrationFailedException : DependencyException
    {
        /// <summary>
        /// Creates empty instance.
        /// </summary>
        public DependencyRegistrationFailedException() 
        { }

        /// <summary>
        /// Creates instance with text message.
        /// </summary>
        /// <param name="message">Text description of the occurred error.</param>
        public DependencyRegistrationFailedException(string message) 
            : base(message) 
        { }

        /// <summary>
        /// Creates instance with text message and inner exception.
        /// </summary>
        /// <param name="message">Text description of the occurred error.</param>
        /// <param name="inner">Source exception that caused problem.</param>
        public DependencyRegistrationFailedException(string message, Exception inner) 
            : base(message, inner) 
        { }

        /// <summary>
        /// Serialization ctor.
        /// </summary>
        /// <param name="info">Info object.</param>
        /// <param name="context">Context object.</param>
        protected DependencyRegistrationFailedException(SerializationInfo info, StreamingContext context)
            : base(info, context) 
        { }
    }
}
