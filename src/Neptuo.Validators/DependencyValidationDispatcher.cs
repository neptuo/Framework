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
        /// For missing handler an <see cref="MissingValidationHandlerException"/> is thrown.
        /// </summary>
        /// <param name="dependencyProvider">Resolver of validation handlers.</param>
        public DependencyValidationDispatcher(IDependencyProvider dependencyProvider)
        {
            Ensure.NotNull(dependencyProvider, "dependencyProvider");
            this.dependencyProvider = dependencyProvider;
        }

        /// <summary>
        /// Creates a new instance and a valid or invalid result is returned for missing handler.
        /// </summary>
        /// <param name="isMissingHandlerValid">Whether a missing handler should return valid result or invalid.</param>
        public DependencyValidationDispatcher(IDependencyProvider dependencyProvider, bool isMissingHandlerValid)
            : base(isMissingHandlerValid)
        {
            Ensure.NotNull(dependencyProvider, "dependencyProvider");
            this.dependencyProvider = dependencyProvider;
        }

        protected override bool TryGetValidationHandler(Type modelType, out object validationHandler)
        {
            try
            {
                Type validatorType = typeof(IValidationHandler<>).MakeGenericType(modelType);
                validationHandler = dependencyProvider.Resolve(validatorType);
                return true;
            }
            catch (DependencyResolutionFailedException)
            {
                validationHandler = null;
                return false;
            }
        }
    }
}
