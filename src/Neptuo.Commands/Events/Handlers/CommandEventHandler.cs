using Neptuo.Pipelines.Events;
using Neptuo.Pipelines.Events.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands.Events.Handlers
{
    public class CommandEventHandler : IEventHandler<CommandHandled>
    {
        private readonly object command;
        private readonly IEventHandler<CommandHandled> innerHandler;

        public CommandEventHandler(object command, IEventHandler<CommandHandled> innerHandler)
        {
            Guard.NotNull(command, "command");
            Guard.NotNull(innerHandler, "innerHandler");
            this.command = command;
            this.innerHandler = innerHandler;
        }

        public void Handle(CommandHandled eventData)
        {
            if (eventData.Command == command)
            {
                //TODO: Unsubscribe.
                //currentManager.UnSubscribe(this);
                innerHandler.Handle(eventData);
            }
        }
    }
}
