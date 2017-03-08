using Neptuo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Validators
{
    /// <summary>
    /// An exception used when a handler for a model is missing.
    /// </summary>
    public class MissingValidationHandlerException : Exception
    {
        /// <summary>
        /// Gets a type of the model without handler.
        /// </summary>
        public Type ModelType { get; private set; }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="modelType">A type of the model without handler.</param>
        public MissingValidationHandlerException(Type modelType)
            : base(string.Format("Missing validation handler for model type '{0}'.", modelType))
        {
            Ensure.NotNull(modelType, "modelType");
            ModelType = modelType;
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="modelType">A type of the model without handler.</param>
        public MissingValidationHandlerException(Type modelType, Exception inner)
            : base(string.Format("Missing validation handler for model type '{0}'.", modelType), inner)
        {
            Ensure.NotNull(modelType, "modelType");
            ModelType = modelType;
        }

        /// <summary>
        /// Creates new instance for deserialization.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The streaming context.</param>
        protected MissingValidationHandlerException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
