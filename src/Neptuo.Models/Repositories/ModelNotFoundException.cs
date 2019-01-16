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
    /// An exception which raises when a requested model with key doesn't exist in the storage.
    /// </summary>
    [Serializable]
    public class ModelNotFoundException : Exception
    {
        /// <summary>
        /// Gets a key of the changed model.
        /// </summary>
        public IKey Key { get; }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="key">A key of the model that has been changed.</param>
        public ModelNotFoundException(IKey key)
            : base($"A model with the key '{key}' doesn't exist.")
        {
            Ensure.NotNull(key, "key");
            Key = key;
        }

        /// <summary>
        /// Creates a new instance for <paramref name="key"/> with root cause in <paramref name="inner"/> exception.
        /// </summary>
        /// <param name="key">A key of the model that has been changed.</param>
        /// <param name="inner">The inner cause of the exceptional state.</param>
        public ModelNotFoundException(IKey key, Exception inner)
            : base($"A model with the key '{key}' doesn't exist.", inner)
        {
            Ensure.NotNull(key, "key");
            Key = key;
        }

        /// <summary>
        /// Creates a new instance for deserialization.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The streaming context.</param>
        protected ModelNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
