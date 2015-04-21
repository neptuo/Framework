using Neptuo.Pipelines.Validators.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Pipelines.Validators
{
    /// <summary>
    /// Validation result builder contract.
    /// </summary>
    public interface IValidationResultBuilder
    {
        /// <summary>
        /// Adds message to the result.
        /// Based on configuration for constructor, this message will or will not invalidate future result.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Self (for fluency).</returns>
        IValidationResultBuilder Add(IValidationMessage message);

        /// <summary>
        /// Adds message to the result.
        /// Based on <paramref name="causesInvalidation" />, this message will or will not invalidate future result.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="causesInvalidation">Flag to see if <paramref name="message"/> invalidates future result.</param>
        /// <returns>Self (for fluency).</returns>
        IValidationResultBuilder Add(IValidationMessage message, bool causesInvalidation);

        /// <summary>
        /// Creates result from this builder.
        /// </summary>
        /// <returns></returns>
        IValidationResult ToResult();
    }
}
