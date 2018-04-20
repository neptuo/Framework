using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators
{
    /// <summary>
    /// Exception that should by thrown by all implementations of <see cref="IDependencyProvider"/> when child scope can't be created.
    /// </summary>
    [Serializable]
    public class DependencyContainerScopeResolutionFailedException : DependencyResolutionFailedException
    {
        /// <summary>
        /// Creates instance with inner exception.
        /// </summary>
        /// <param name="inner">Source exception that caused problem.</param>
        public DependencyContainerScopeResolutionFailedException(Type requiredType, Exception inner) 
            : base(requiredType, inner)
        { }

        /// <summary>
        /// Serialization ctor.
        /// </summary>
        /// <param name="info">Info object.</param>
        /// <param name="context">Context object.</param>
        protected DependencyContainerScopeResolutionFailedException(SerializationInfo info, StreamingContext context)
            : base(info, context) 
        { }
    }
}
