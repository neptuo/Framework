using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Validators
{
    /// <summary>
    /// Validation exception.
    /// This class should be used for throwing errors in validation.
    /// </summary>
    public class ValidationException : Exception
    {
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
    }
}
