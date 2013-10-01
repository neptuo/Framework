using Neptuo.Data.Commands.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Commands
{
    public class DependencyCommandDispatcher : ICommandDispatcher
    {
        private IDependencyProvider dependencyProvider;

        public DependencyCommandDispatcher(IDependencyProvider dependencyProvider)
        {
            this.dependencyProvider = dependencyProvider;
        }

        public void Handle<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            ICommandHandler<TCommand> handler = dependencyProvider.Resolve<ICommandHandler<TCommand>>();
            handler.Handle(command);
        }
    }
}
