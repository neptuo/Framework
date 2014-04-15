using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Validation
{
    public class DependencyValidatorService : IValidatorService
    {
        private readonly IDependencyProvider dependencyProvider;

        public DependencyValidatorService(IDependencyProvider dependencyProvider)
        {
            Guard.NotNull(dependencyProvider, "dependencyProvider");
            this.dependencyProvider = dependencyProvider;
        }

        public IValidationResult Validate<TModel>(TModel model)
        {
            IValidator<TModel> validator = dependencyProvider.Resolve<IValidator<TModel>>();
            return validator.Validate(model);
        }
    }
}
