using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Validation
{
    /// <summary>
    /// Validator for instances of <typeparamref name="TModel"/>.
    /// </summary>
    /// <typeparam name="TModel">Type of model to validate.</typeparam>
    public interface IValidator<TModel>
    {
        /// <summary>
        /// Validates <paramref name="model"/>.
        /// Returns validation result.
        /// </summary>
        /// <param name="model">Instance to validate.</param>
        /// <returns>Validation result.</returns>
        IValidationResult Validate(TModel model);
    }
}
