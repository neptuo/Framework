using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Validators
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
        /// <param name="model">An instance to validate.</param>
        /// <returns><see cref="IValidationResult"/> describing success or validation failure.</returns>
        Task<IValidationResult> ValidateAsync<TModel>(TModel model);

        /// <summary>
        /// Validates <paramref name="model"/> using runtime type of model.
        /// Best for framework code where TModel can't be determined.
        /// </summary>
        /// <param name="model">An instance to validate.</param>
        /// <returns><see cref="IValidationResult"/> describing success or validation failure.</returns>
        Task<IValidationResult> ValidateAsync(object model);
    }
}
