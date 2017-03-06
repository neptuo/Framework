using Neptuo;
using Neptuo.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands
{
    /// <summary>
    /// An implementation of <see cref="ICommandDispatcher"/> which uses <see cref="IValidationDispatcher"/> to validate all incoming commands.
    /// </summary>
    public class ValidationCommandDispatcher : ICommandDispatcher
    {
        private readonly IValidationDispatcher validationDispatcher;
        private readonly ICommandDispatcher innerDispatcher;

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="validationDispatcher">A validation dispatcher used for validating commands.</param>
        /// <param name="innerDispatcher">An inner (real) commands dispatcher for validated commands handling.</param>
        public ValidationCommandDispatcher(IValidationDispatcher validationDispatcher, ICommandDispatcher innerDispatcher)
        {
            Ensure.NotNull(validationDispatcher, "validationDispatcher");
            Ensure.NotNull(innerDispatcher, "innerDispatcher");
            this.validationDispatcher = validationDispatcher;
            this.innerDispatcher = innerDispatcher;
        }

        public async Task HandleAsync<TCommand>(TCommand command)
        {
            await ValidateAsync(command);
            await innerDispatcher.HandleAsync(command);
        }

        /// <summary>
        /// A method responsible for validating commands.
        /// </summary>
        /// <typeparam name="TCommand">A type of the command.</typeparam>
        /// <param name="command">An instance of command to validate.</param>
        /// <returns>A continuation task.</returns>
        protected virtual async Task ValidateAsync<TCommand>(TCommand command)
        {
            IValidationResult result = await validationDispatcher.ValidateAsync(command);
            result.ThrowIfNotValid();
        }
    }
}
