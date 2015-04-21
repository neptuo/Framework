using Neptuo.Pipelines.Validators.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Pipelines.Validators
{
    /// <summary>
    /// Builder for <see cref="IValidationResult"/>
    /// </summary>
    public class ValidationResultBuilder : IValidationResult, IValidationResultBuilder
    {
        private readonly List<IValidationMessage> messages = new List<IValidationMessage>();
        private readonly bool isInvalidationCausedByAnyMessage;
        private bool isValid = true;

        /// <summary>
        /// Creates new instance of builder.
        /// </summary>
        /// <param name="isInvalidationCausedByAnyMessage">
        /// If <c>true</c>, adding any message will invalidate future result.
        /// If <c>false</c>, only explicitly maked messages causes invalidation of result.
        /// </param>
        public ValidationResultBuilder(bool isInvalidationCausedByAnyMessage)
        {
            this.isInvalidationCausedByAnyMessage = isInvalidationCausedByAnyMessage;
        }

        /// <summary>
        /// Adds message to the result.
        /// Based on configuration for constructor, this message will or will not invalidate future result.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Self (for fluency).</returns>
        public ValidationResultBuilder Add(IValidationMessage message)
        {
            return Add(message, isInvalidationCausedByAnyMessage);
        }

        /// <summary>
        /// Adds message to the result.
        /// Based on <paramref name="causesInvalidation" />, this message will or will not invalidate future result.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="causesInvalidation">Flag to see if <paramref name="message"/> invalidates future result.</param>
        /// <returns>Self (for fluency).</returns>
        public ValidationResultBuilder Add(IValidationMessage message, bool causesInvalidation)
        {
            Ensure.NotNull(message, "message");
            messages.Add(message);

            if (causesInvalidation)
                isValid = false;

            return this;
        }

        public IValidationResult ToResult()
        {
            return this;
        }

        public override string ToString()
        {
            return String.Format("{0} ({1})", isValid ? "Valid" : "InValid", messages.Count);
        }

        #region IValidationResult

        bool IValidationResult.IsValid
        {
            get { return isValid; }
        }

        IEnumerable<IValidationMessage> IValidationResult.Messages
        {
            get { return messages; }
        }

        #endregion

        #region IValidationResultBuilder

        IValidationResultBuilder IValidationResultBuilder.Add(IValidationMessage message)
        {
            return Add(message);
        }

        IValidationResultBuilder IValidationResultBuilder.Add(IValidationMessage message, bool causesInvalidation)
        {
            return Add(message, causesInvalidation);
        }

        IValidationResult IValidationResultBuilder.ToResult()
        {
            return this;
        }

        #endregion
    }
}
