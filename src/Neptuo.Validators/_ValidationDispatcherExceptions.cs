using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Validators
{
    /// <summary>
    /// A common extensions for the <see cref="IValidationDispatcher"/>
    /// </summary>
    public static class _ValidationDispatcherExceptions
    {
        /// <summary>
        /// Validates <paramref name="model"/> synchronously and returns validation result.
        /// </summary>
        /// <typeparam name="TModel">Type of model to validate.</typeparam>
        /// <param name="dispatcher">A validation dispatcher.</param>
        /// <param name="model">An instance to validate.</param>
        /// <returns><see cref="IValidationResult"/> describing success or validation failure.</returns>
        public static IValidationResult Validate<TModel>(this IValidationDispatcher dispatcher, TModel model)
        {
            try
            {
                Task<IValidationResult> task = dispatcher.ValidateAsync(model);
                task.Wait();
                return task.Result;
            }
            catch (AggregateException e)
            {
                throw e.InnerException;
            }
        }

        /// <summary>
        /// Validates <paramref name="model"/> synchronously using runtime type of model.
        /// Best for framework code where TModel can't be determined.
        /// </summary>
        /// <typeparam name="TModel">Type of model to validate.</typeparam>
        /// <param name="dispatcher">A validation dispatcher.</param>
        /// <param name="model">An instance to validate.</param>
        /// <returns><see cref="IValidationResult"/> describing success or validation failure.</returns>
        public static IValidationResult Validate(this IValidationDispatcher dispatcher, object model)
        {
            try
            {
                Task<IValidationResult> task = dispatcher.ValidateAsync(model);
                task.Wait();
                return task.Result;
            }
            catch (AggregateException e)
            {
                throw e.InnerException;
            }
        }
    }
}
