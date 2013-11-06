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

        public void Handle<TCommand>(TCommand command)
        {
            ICommandHandler<TCommand> handler = dependencyProvider.Resolve<ICommandHandler<TCommand>>();
            handler.Handle(command);
        }

        public IValidationResult Validate<TCommand>(TCommand command)
        {
            IValidator<TCommand> validator = dependencyProvider.Resolve<IValidator<TCommand>>();
            return validator.Validate(command);
        }
    }
}
