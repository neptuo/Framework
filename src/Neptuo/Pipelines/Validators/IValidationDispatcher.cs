using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Pipelines.Validators
{
    /// <summary>
    /// Front facade for providing validations.
    /// </summary>
    public interface IValidationDispatcher
    {
        /// <summary>
        /// Validates <paramref name="model"/> and returns validation result.
        /// </summary>
        /// <typeparam name="TModel">Type of model to validate.</typeparam>
        /// <param name="model">Model instance to validate.</param>
        /// <returns><see cref="IValidationResult"/> describing succes or validation failure.</returns>
        IValidationResult Validate<TModel>(TModel model);

        /// <summary>
        /// Validates <paramref name="model"/> using runtime type of model.
        /// Best for framework code where TModel can't be determined.
        /// </summary>
        /// <param name="model">Model instance to validate.</param>
        /// <returns><see cref="IValidationResult"/> describing succes or validation failure.</returns>
        IValidationResult Validate(object model);
    }
}
