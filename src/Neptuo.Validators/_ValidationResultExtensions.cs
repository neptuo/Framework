using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Validators
{
    /// <summary>
    /// A common exceptions for the <see cref="IValidationResult"/>.
    /// </summary>
    public static class _ValidationResultExtensions
    {
        /// <summary>
        /// Throws a <see cref="ValidationException"/> if the <paramref name="result"/> is not valid.
        /// </summary>
        /// <param name="result">A result to throw exception for.</param>
        public static void ThrowIfNotValid(this IValidationResult result)
        {
            ValidationException.ThrowIfNotValid(result);
        }
    }
}
