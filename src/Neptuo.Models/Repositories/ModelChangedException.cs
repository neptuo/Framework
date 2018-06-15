using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Repositories
{
    /// <summary>
    /// An exception which raises when a model in the storage has been changed by somebody else.
    /// </summary>
    [Serializable]
    public class ModelChangedException : Exception
    {
        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="key">A key of the model that has been changed.</param>
        public ModelChangedException(IKey key)
            : base($"A model with the key '{key}' has been changed.")
        { }

        /// <summary>
        /// Creates a new instance for <paramref name="key"/> with root cause in <paramref name="inner"/> exception.
        /// </summary>
        /// <param name="key">A key of the model that has been changed.</param>
        /// <param name="inner">The inner cause of the exceptional state.</param>
        public ModelChangedException(IKey key, Exception inner)
            : base($"A model with the key '{key}' has been changed.", inner)
        { }

        /// <summary>
        /// Creates a new instance for deserialization.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The streaming context.</param>
        protected ModelChangedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
