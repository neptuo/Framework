using Neptuo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Validators
{
    /// <summary>
    /// Validation exception.
    /// This class should be used for throwing errors in validation.
    /// </summary>
    public class ValidationException : Exception
    {
        /// <summary>
        /// Gets a result of the validation.
        /// </summary>
        public IValidationResult Result { get; private set; }

        /// <summary>
        /// Creates new instance with <paramref name="result"/>.
        /// </summary>
        /// <param name="result">Result of validation process.</param>
        public ValidationException(IValidationResult result)
        {
            Ensure.NotNull(result, "result");
            Result = result;
        }

        /// <summary>
        /// Throws a <see cref="ValidationException"/> if the <paramref name="result"/> is not valid.
        /// </summary>
        /// <param name="result">A result to throw exception for.</param>
        public static void ThrowIfNotValid(IValidationResult result)
        {
            Ensure.NotNull(result, "result");

            if (result.IsValid)
                return;

            throw new ValidationException(result);
        }
    }
}
