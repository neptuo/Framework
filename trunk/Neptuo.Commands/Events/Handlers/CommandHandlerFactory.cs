using Neptuo.Events.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands.Events.Handlers
{
    public class CommandHandlerFactory : IEventHandlerFactory<CommandHandled>
    {
        private object command;
        private IEventHandlerFactory<CommandHandled> innerFactory;

        public CommandHandlerFactory(object command, IEventHandlerFactory<CommandHandled> innerFactory)
        {
            Guard.NotNull(command, "command");
            Guard.NotNull(innerFactory, "innerFactory");
            this.command = command;
            this.innerFactory = innerFactory;
        }

        public IEventHandler<CommandHandled> CreateHandler(CommandHandled eventData)
        {
            if (eventData.Command == command)
                return innerFactory.CreateHandler(eventData);

            return null;
        }
    }
}
