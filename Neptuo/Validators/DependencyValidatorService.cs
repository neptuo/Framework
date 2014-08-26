using Neptuo.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Validators
{
    public class DependencyValidatorService : IValidationDispatcher
    {
        private static readonly string ValidateMethodName = "Validate"; //TypeHelper.MethodName<IValidator<object>, object, IValidationResult>(v => v.Validate)

        private readonly IDependencyProvider dependencyProvider;

        public DependencyValidatorService(IDependencyProvider dependencyProvider)
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
