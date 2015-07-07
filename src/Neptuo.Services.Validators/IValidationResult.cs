using Neptuo.Services.Validators;
using Neptuo.Services.Validators.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Validators
{
    /// <summary>
    /// Describes result of validation process.
    /// </summary>
    public interface IValidationResult
    {
        /// <summary>
        /// Flag to see if validation was successfull (= model was valid).
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// Enumeration of messages created by validation process.
        /// Can also contain non-error message (eg. warnings).
        /// </summary>
        IEnumerable<IValidationMessage> Messages { get; }
    }
}
