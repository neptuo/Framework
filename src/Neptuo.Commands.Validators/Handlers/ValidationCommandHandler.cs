using Neptuo;
using Neptuo.Validators;
using Neptuo.Validators.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands.Handlers
{
    /// <summary>
    /// An implementation of <see cref="ICommandHandler{TCommand}"/> which uses <see cref="IValidationDispatcher"/> or <see cref="IValidationHandler{TModel}"/> to validate all incoming commands.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ValidationCommandHandler<T> : ICommandHandler<T>
    {
        private readonly IValidationDispatcher validationDispatcher;
        private readonly IValidationHandler<T> validationHandler;
        private readonly ICommandHandler<T> innerHandler;

        /// <summary>
        /// Creates a new instance which uses <paramref name="validationDispatcher"/> to validate all incoming commands.
        /// </summary>
        /// <param name="validationDispatcher">A validation dispatcher used for validating commands.</param>
        /// <param name="innerDispatcher">An inner (real) commands handler for validated commands handling.</param>
        public ValidationCommandHandler(IValidationDispatcher validationDispatcher, ICommandHandler<T> innerHandler)
        {
            Ensure.NotNull(validationDispatcher, "validationDispatcher");
            Ensure.NotNull(innerHandler, "innerHandler");
            this.validationDispatcher = validationDispatcher;
            this.innerHandler = innerHandler;
        }

        /// <summary>
        /// Creates a new instance which uses <paramref name="validationHandler"/> to validate all incoming commands.
        /// </summary>
        /// <param name="validationDispatcher">A validation handler used for validating commands.</param>
        /// <param name="innerDispatcher">An inner (real) commands handler for validated commands handling.</param>
        public ValidationCommandHandler(IValidationHandler<T> validationHandler, ICommandHandler<T> innerHandler)
        {
            Ensure.NotNull(validationHandler, "validationHandler");
            Ensure.NotNull(innerHandler, "innerHandler");
            this.validationHandler = validationHandler;
            this.innerHandler = innerHandler;
        }

        public async Task HandleAsync(T command)
        {
            await ValidateAsync(command);
            await innerHandler.HandleAsync(command);
        }

        protected virtual async Task ValidateAsync(T command)
        {
            if (validationHandler != null)
                await validationHandler.HandleAsync(command);

            if (validationDispatcher != null)
                await validationDispatcher.ValidateAsync(command);
        }
    }
}
