using Neptuo.Pipelines.Events;
using Neptuo.Pipelines.Events.Handlers;
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

        public IEventHandler<CommandHandled> CreateHandler(CommandHandled eventData, IEventManager currentManager)
        {
            if (eventData.Command == command)
            {
                currentManager.UnSubscribe(this);
                return innerFactory.CreateHandler(eventData, currentManager);
            }

            return null;
        }
    }
}
