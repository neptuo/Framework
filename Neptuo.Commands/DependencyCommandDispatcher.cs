using Neptuo.Commands.Handlers;
using Neptuo.Commands.Validation;
using Neptuo.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands
{
    /// <summary>
    /// Command dispatcher based on registration from <see cref="IDependencyProvider"/>.
    /// </summary>
    public class DependencyCommandDispatcher : ICommandDispatcher
    {
        private IDependencyProvider dependencyProvider;

        /// <summary>
        /// Initializes new instance with <paramref name="dependencyProvider"/> as source for registrations of <see cref="ICommandHandler<>"/>.
        /// </summary>
        /// <param name="dependencyProvider">Source for registrations of <see cref="ICommandHandler<>"/>.</param>
        public DependencyCommandDispatcher(IDependencyProvider dependencyProvider)
        {
            this.dependencyProvider = dependencyProvider;
        }

        /// <summary>
        /// Handles <paramref name="command"/>.
        /// </summary>
        /// <typeparam name="TCommand">Type of command to handle.</typeparam>
        /// <param name="command">Command to handle.</param>
        public void Handle<TCommand>(TCommand command)
        {
            try
            {
                ICommandHandler<TCommand> handler = dependencyProvider.Resolve<ICommandHandler<TCommand>>();
                handler.Handle(command);
            }
            catch (Exception e)
            {
                HandleException(e, ExceptionSource.FromHandle);
            }
        }

        /// <summary>
        /// Validates <paramref name="command"/>.
        /// </summary>
        /// <typeparam name="TCommand">Type of command to validate.</typeparam>
        /// <param name="command">Command to validate.</param>
        /// <returns>Validation result.</returns>
        public IValidationResult Validate<TCommand>(TCommand command)
        {
            try
            {
                IValidator<TCommand> validator = dependencyProvider.Resolve<IValidator<TCommand>>();
                return validator.Validate(command);
            }
            catch (Exception e)
            {
                HandleException(e, ExceptionSource.FromValidate);
                return new ValidationResultBase(false);
            }
        }

        /// <summary>
        /// Handles exceptions occured while handling or validating command.
        /// </summary>
        /// <param name="e">Exception that occured.</param>
        /// <param name="source">Whether Handle or Validate method caused exception.</param>
        protected virtual void HandleException(Exception e, ExceptionSource source)
        {
            ICommandHandler<Exception> exceptionHandler = dependencyProvider.Resolve<ICommandHandler<Exception>>();
            exceptionHandler.Handle(e);
        }

        /// <summary>
        /// Enumeration of methods where exception can occur while dispatching command.
        /// </summary>
        public enum ExceptionSource
        {
            FromHandle,
            FromValidate,
        }
    }
}
