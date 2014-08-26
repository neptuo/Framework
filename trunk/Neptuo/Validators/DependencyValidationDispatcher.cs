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
    /// </summary>
    public class DependencyValidationDispatcher : IValidationDispatcher
    {
        /// <summary>
        /// Name of <see cref="IValidationHandler.Validate"/>.
        /// </summary>
        /// <remarks>
        /// Because of SharpKit can't be defined by <see cref="TypeHelper"/>.
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
            IValidationHandler<TModel> validator = dependencyProvider.Resolve<IValidationHandler<TModel>>();
            return validator.Validate(model);
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
