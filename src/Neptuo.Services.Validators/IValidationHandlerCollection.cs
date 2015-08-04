using Neptuo.Services.Validators.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Validators
{
    /// <summary>
    /// Collection of registered validation handlers.
    /// </summary>
    public interface IValidationHandlerCollection
    {
        /// <summary>
        /// Registers <paramref name="handler"/> to handle validation for models of type <typeparamref name="TModel"/>.
        /// </summary>
        /// <typeparam name="TModel">Type of model.</typeparam>
        /// <param name="handler">Handler for validation for model of type <typeparamref name="TQuery"/>.</param>
        /// <returns>Self (for fluency).</returns>
        IValidationHandlerCollection Add<TModel>(IValidationHandler<TModel> handler);

        /// <summary>
        /// Tries to find query handler for query of type <typeparamref name="TQuery"/>.
        /// </summary>
        /// <typeparam name="TModel">Type of model.</typeparam>
        /// <param name="handler">Handler for validation for model of type <typeparamref name="TQuery"/>.</param>
        /// <returns><c>true</c> if such a handler is registered; <c>false</c> otherwise.</returns>
        bool TryGet<TModel>(out IValidationHandler<TModel> handler);
    }
}
