using Neptuo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators
{
    /// <summary>
    /// Exception that should by thrown by all implementations of <see cref="IDependencyProvider"/> when instance can't provided.
    /// </summary>
    [Serializable]
    public class DependencyResolutionFailedException : DependencyException
    {
        /// <summary>
        /// Gets a type on which resolution failed.
        /// </summary>
        public Type RequiredType { get; }

        /// <summary>
        /// Creates a instance with <paramref name="requiredType"/> as a type on which resolution failed.
        /// </summary>
        /// <param name="requiredType">A type on which resolution failed.</param>
        public DependencyResolutionFailedException(Type requiredType)
            : base($"Problem resolving type '{requiredType?.FullName}'.")
        {
            Ensure.NotNull(requiredType, "requiredType");
            RequiredType = requiredType;
        }

        /// <summary>
        /// Creates a instance with <paramref name="requiredType"/> as a type on which resolution failed and original cause in <paramref name="inner"/>.
        /// </summary>
        /// <param name="requiredType">A type on which resolution failed.</param>
        /// <param name="inner">Source exception that caused problem.</param>
        public DependencyResolutionFailedException(Type requiredType, Exception inner) 
            : base($"Problem resolving type '{requiredType?.FullName}'.", inner) 
        {
            Ensure.NotNull(requiredType, "requiredType");
            RequiredType = requiredType;
        }

        /// <summary>
        /// Serialization ctor.
        /// </summary>
        /// <param name="info">Info object.</param>
        /// <param name="context">Context object.</param>
        protected DependencyResolutionFailedException(SerializationInfo info, StreamingContext context)
            : base(info, context) 
        { }
    }
}
