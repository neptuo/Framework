using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Validators.Internals
{
    /// <summary>
    /// Definition of command handler inside <see cref="DefaultValidationDispatcher"/>.
    /// </summary>
    public class DefaultValidationHandlerDefinition
    {
        /// <summary>
        /// Validation handler.
        /// Should never be <c>null</c>.
        /// </summary>
        public object ValidationHandler { get; set; }

        public Func<object, Task<IValidationResult>> HandleMethod { get; set; }

        public DefaultValidationHandlerDefinition(object commandHandler, Func<object, Task<IValidationResult>> handleMethod)
        {
            ValidationHandler = commandHandler;
            HandleMethod = handleMethod;
        }

        public Task<IValidationResult> HandleAsync(object command)
        {
            return HandleMethod(command);
        }
    }
}
