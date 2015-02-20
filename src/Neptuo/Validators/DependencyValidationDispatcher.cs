using Neptuo.DomainModels;
using Neptuo.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Validators
{
    /// <summary>
    /// Base implementation of <see cref="IValidationDispatcher"/> using <see cref="IDependencyProvider"/>.
    /// Before and after validation also uses and sets <see cref="IValidatableModel"/>.
    /// </summary>
    public class DependencyValidationDispatcher : IValidationDispatcher
    {
        /// <summary>
        /// Name of the <see cref="IValidationHandler.Validate"/>.
        /// </summary>
        /// <remarks>
        /// Because of SharpKit, this can't be defined by <see cref="TypeHelper"/>.
        /// </remarks>
        private static readonly string ValidateMethodName = "Validate"; //TypeHelper.MethodName<IValidator<object>, object, IValidationResult>(v => v.Validate)

        /// <summary>
        /// Inner provider of validation handlers.
        /// </summary>
        private readonly IDependencyProvider dependencyProvider;

        /// <summary>
        /// Creates new instance using <paramref name="dependencyProvider"/> for resolving validation handlers.
        /// </summary>
        /// <param name="dependencyProvider">Resolver of validation handlers.</param>
        public DependencyValidationDispatcher(IDependencyProvider dependencyProvider)
        {
            Guard.NotNull(dependencyProvider, "dependencyProvider");
            this.dependencyProvider = dependencyProvider;
        }

        public IValidationResult Validate<TModel>(TModel model)
        {
            IValidatableModel validatable = model as IValidatableModel;
            if (validatable != null && validatable.IsValid)
                return new ValidationResultBase(true);

            IValidationHandler<TModel> validator = dependencyProvider.Resolve<IValidationHandler<TModel>>();
            IValidationResult result = validator.Validate(model);

            if (validatable != null)
                validatable.IsValid = result.IsValid;

            return result;
        }

        public IValidationResult Validate(object model)
        {
            Guard.NotNull(model, "model");
            Type modelType = model.GetType();
            Type validatorType = typeof(IValidationHandler<>).MakeGenericType(modelType);
            MethodInfo validateMethod = validatorType.GetMethod(ValidateMethodName);
            
            object validator = dependencyProvider.Resolve(validatorType);
            object validationResult = validateMethod.Invoke(validator, new object[] { model });
            return (IValidationResult)validationResult;
        }
    }
}
