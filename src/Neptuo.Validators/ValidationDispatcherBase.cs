using Neptuo.Validators.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Neptuo.Validators.Messages;

namespace Neptuo.Validators
{
    /// <summary>
    /// Base implementation of <see cref="IValidationDispatcher"/> based on registration of <see cref="IValidationHandler"/> for type of model.
    /// </summary>
    public abstract class ValidationDispatcherBase : IValidationDispatcher
    {
        /// <summary>
        /// Name of the <see cref="Handlers.IValidationHandler{T}.HandleAsync"/>.
        /// </summary>
        /// <remarks>
        /// Because of SharpKit, this can't be defined by <see cref="TypeHelper"/>.
        /// </remarks>
        private static readonly string validateMethodName = "ValidateAsync"; //TypeHelper.MethodName<IValidator<object>, object, IValidationResult>(v => v.Validate)

        private readonly bool? isMissingHandlerValid;

        /// <summary>
        /// Creates a new instance and an exception is thrown for missing handler.
        /// </summary>
        protected ValidationDispatcherBase()
        { }

        /// <summary>
        /// Creates a new instance and a valid or invalid result is returned for missing handler.
        /// </summary>
        /// <param name="isMissingHandlerValid">Whether a missing handler should return valid result or invalid.</param>
        protected ValidationDispatcherBase(bool isMissingHandlerValid)
        {
            this.isMissingHandlerValid = isMissingHandlerValid;
        }

        public async Task<IValidationResult> ValidateAsync<TModel>(TModel model)
        {
            Type modelType = typeof(TModel);
            object validationHandler;
            if (TryGetValidationHandler(modelType, out validationHandler))
            {
                IValidationHandler<TModel> validator = (IValidationHandler<TModel>)validationHandler;
                IValidationResult result = await validator.HandleAsync(model);

                return result;
            }

            return ProcessMissingHandler(model);
        }

        public Task<IValidationResult> ValidateAsync(object model)
        {
            Ensure.NotNull(model, "model");
            Type modelType = model.GetType();

            object validationHandler;
            if (TryGetValidationHandler(modelType, out validationHandler))
            {
                Type validatorType = typeof(IValidationHandler<>).MakeGenericType(modelType);
                MethodInfo validateMethod = validatorType.GetMethod(validateMethodName);

                object validationResult = validateMethod.Invoke(validationHandler, new object[] { model });
                return (Task<IValidationResult>)validationResult;
            }

            return Task.FromResult(ProcessMissingHandler(model));
        }

        /// <summary>
        /// Used when there isn't a handler for the <paramref name="model"/>.
        /// </summary>
        /// <param name="model">A model without handler.</param>
        /// <returns>A validation result.</returns>
        protected virtual IValidationResult ProcessMissingHandler(object model)
        {
            if (isMissingHandlerValid == null)
            {
                Type modelType = model.GetType();
                throw new MissingValidationHandlerException(modelType);
            }

            return new ValidationResult(isMissingHandlerValid.Value);
        }

        /// <summary>
        /// Tries to find validation handler for models of type <paramref name="modelType"/>.
        /// </summary>
        /// <param name="modelType">Type of model to find handler for.</param>
        /// <param name="validationHandler">Validation for models of type <paramref name="modelType"/>.</param>
        /// <returns><c>true</c> if such handler is registered; <c>false</c> otherwise.</returns>
        protected abstract bool TryGetValidationHandler(Type modelType, out object validationHandler);

        private class ValidationResult : IValidationResult
        {
            public bool IsValid { get; private set; }
            public IEnumerable<IValidationMessage> Messages { get; private set; }

            public ValidationResult(bool isValid)
            {
                IsValid = isValid;
                Messages = Enumerable.Empty<IValidationMessage>();
            }
        }
    }
}
