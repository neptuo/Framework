using Neptuo.Activators;
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
    /// Base implementation of <see cref="IValidationDispatcher"/> using <see cref="IDependencyProvider"/>.
    /// Before and after validation also uses and sets <see cref="IValidatableModel"/>.
    /// </summary>
    public class DependencyValidationDispatcher : ValidationDispatcherBase
    {
        private readonly IDependencyProvider dependencyProvider;

        /// <summary>
        /// Creates new instance using <paramref name="dependencyProvider"/> for resolving validation handlers.
        /// </summary>
        /// <param name="dependencyProvider">Resolver of validation handlers.</param>
        public DependencyValidationDispatcher(IDependencyProvider dependencyProvider)
        {
            Ensure.NotNull(dependencyProvider, "dependencyProvider");
            this.dependencyProvider = dependencyProvider;
        }

        protected override bool TryGetValidationHandler(Type modelType, out object validationHandler)
        {
            Type validatorType = typeof(IValidationHandler<>).MakeGenericType(modelType);
            validationHandler = dependencyProvider.Resolve(validatorType);
            return true;
        }
    }
}
