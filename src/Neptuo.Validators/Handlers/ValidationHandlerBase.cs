using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Validators.Handlers
{
    /// <summary>
    /// A base class for synchronous implementation of <see cref="IValidationHandler{TModel}"/>.
    /// </summary>
    /// <typeparam name="TModel">A type of the model to validate.</typeparam>
    public abstract class ValidationHandlerBase<TModel> : IValidationHandler<TModel>
    {
        private readonly bool isInvalidationCausedByAnyMessage;

        /// <summary>
        /// Creates a new instance where all messages marks the result as failed.
        /// </summary>
        protected ValidationHandlerBase()
            : this(true)
        { }

        /// <summary>
        /// Creates a new instance where <paramref name="isInvalidationCausedByAnyMessage"/> indicates how handle result invalidation.
        /// </summary>
        /// <param name="isInvalidationCausedByAnyMessage">If <c>true</c>, all messages marks result as invalid.</param>
        protected ValidationHandlerBase(bool isInvalidationCausedByAnyMessage)
        {
            this.isInvalidationCausedByAnyMessage = isInvalidationCausedByAnyMessage;
        }

        /// <summary>
        /// Validates <paramref name="model"/> and returns validation result.
        /// </summary>
        /// <param name="model">An instance to validate.</param>
        /// <returns><see cref="IValidationResult"/> describing success or validation failure.</returns>
        protected virtual IValidationResult Handle(TModel model)
        {
            ValidationResultBuilder builder = new ValidationResultBuilder(isInvalidationCausedByAnyMessage);
            Handle(builder, model);
            return builder.ToResult();
        }

        /// <summary>
        /// Validates <paramref name="models"/> to builds the result using <paramref name="builder"/>.
        /// </summary>
        /// <param name="builder">A validation result builder.</param>
        /// <param name="model">An instance to validate.</param>
        protected virtual void Handle(IValidationResultBuilder builder, TModel model)
        { }

        public virtual Task<IValidationResult> HandleAsync(TModel model)
        {
            return Task.FromResult(Handle(model));
        }
    }
}
