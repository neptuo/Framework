using Neptuo.Pipelines.Validators;
using Neptuo.Pipelines.Validators.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Pipelines.Validators
{
    /// <summary>
    /// Base implementation of <see cref="IValidationResult"/>.
    /// Supports warning messages.
    /// </summary>
    public class ValidationResultBase : IValidationResult
    {
        public bool IsValid { get; set; }
        public IEnumerable<IValidationMessage> Messages { get; set; }

        /// <summary>
        /// Creates new instance with <paramref name="isValid"/> as validation success/failure flag and empty message collection.
        /// </summary>
        /// <param name="isValid">Whether validation was successfull.</param>
        public ValidationResultBase(bool isValid)
            : this(isValid, new List<IValidationMessage>())
        { }

        /// <summary>
        /// Creates new instance with <paramref name="isValid"/> as validation success/failure flag and <paramref name="messages"/> as message collection.
        /// </summary>
        /// <param name="isValid">Whether validation was successfull.</param>
        /// <param name="messages">Collection of validation messages.</param>
        public ValidationResultBase(bool isValid, IEnumerable<IValidationMessage> messages)
        {
            Ensure.NotNull(messages, "messages");
            IsValid = isValid;
            Messages = messages;
        }
    }
}
