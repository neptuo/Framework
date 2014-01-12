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
    public class DependencyCommandDispatcher : ICommandDispatcher
    {
        private IDependencyProvider dependencyProvider;

        public DependencyCommandDispatcher(IDependencyProvider dependencyProvider)
        {
            this.dependencyProvider = dependencyProvider;
        }

        public Task<TResult> Handle<TResult, TCommand>(TCommand command)
        {
            ICommandHandler<TResult, TCommand> handler = dependencyProvider.Resolve<ICommandHandler<TResult, TCommand>>();
            return handler.Handle(command);
        }

        public IValidationResult Validate<TCommand>(TCommand command)
        {
            IValidator<TCommand> validator = dependencyProvider.Resolve<IValidator<TCommand>>();
            return validator.Validate(command);
        }
    }
}
