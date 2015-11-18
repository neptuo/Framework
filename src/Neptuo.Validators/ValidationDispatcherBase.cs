using Neptuo.Models;
using Neptuo.Validators.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<IValidationResult> ValidateAsync<TModel>(TModel model)
        {
            IValidatableModel validatable = model as IValidatableModel;
            if (validatable != null)
            {
                if (validatable.IsValid != null)
                    return new ValidationResult(validatable.IsValid.Value);
            }

            Type modelType = typeof(TModel);
            object validationHandler;
            if (TryGetValidationHandler(modelType, out validationHandler))
            {
                IValidationHandler<TModel> validator = (IValidationHandler<TModel>)validationHandler;
                IValidationResult result = await validator.HandleAsync(model);

                if (validatable != null)
                    validatable.IsValid = result.IsValid;

                return result;
            }

            throw Ensure.Exception.ArgumentOutOfRange("model", "There isn't validation handler for model of type '{0}'.", modelType.FullName);
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

            throw Ensure.Exception.ArgumentOutOfRange("model", "There isn't validation handler for model of type '{0}'.", modelType.FullName);
        }

        /// <summary>
        /// Tries to find validation handler for models of type <paramref name="modelType"/>.
        /// </summary>
        /// <param name="modelType">Type of model to find handler for.</param>
        /// <param name="validationHandler">Validation for models of type <paramref name="modelType"/>.</param>
        /// <returns><c>true</c> if such handler is registered; <c>false</c> otherwise.</returns>
        protected abstract bool TryGetValidationHandler(Type modelType, out object validationHandler);
    }
}
