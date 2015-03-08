using Neptuo.Pipelines.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Validators
{
    /// <summary>
    /// Build validation result that cosists of validation messages.
    /// </summary>
    public interface IModelValidationBuilder
    {
        /// <summary>
        /// Adds single validation message and returns self (for fluency).
        /// </summary>
        /// <param name="message">New message to add. Can't be null.</param>
        /// <returns>Self (for fluency).</returns>
        IModelValidationBuilder AddMessage(IValidationMessage message);

        /// <summary>
        /// Adds bunch of validation messages and returns self (for fluency).
        /// </summary>
        /// <param name="messages">News messages to add. Can't be null.</param>
        /// <returns>Self (for fluency).</returns>
        IModelValidationBuilder AddMessages(IEnumerable<IValidationMessage> messages);

        /// <summary>
        /// Builds validation result from this context object.
        /// </summary>
        /// <returns>Validation result from this context object.</returns>
        IValidationResult ToResult();
    }
}
