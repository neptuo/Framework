using Neptuo.ComponentModel;
using Neptuo.Pipelines.Events;
using Neptuo.Pipelines.Events.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Pipelines.Commands.Events.Handlers
{
    public class CommandEventHandler : IEventHandler<IEventHandlerContext<CommandHandled>>
    {
        private readonly object command;
        private readonly IEventHandler<CommandHandled> innerHandler;
        private readonly IEventHandler<Envelope<CommandHandled>> innerEnvelopeHandler;

        public CommandEventHandler(object command, IEventHandler<CommandHandled> innerHandler)
        {
            Guard.NotNull(command, "command");
            Guard.NotNull(innerHandler, "innerHandler");
            this.command = command;
            this.innerHandler = innerHandler;
        }

        public CommandEventHandler(object command, IEventHandler<Envelope<CommandHandled>> innerHandler)
        {
            Guard.NotNull(command, "command");
            Guard.NotNull(innerHandler, "innerHandler");
            this.command = command;
            this.innerEnvelopeHandler = innerHandler;
        }

        public Task HandleAsync(IEventHandlerContext<CommandHandled> context)
        {
            if (context.Payload.Body.Command == command)
            {
                context.Registry.UnSubscribe(this);

                if (innerHandler != null)
                    innerHandler.HandleAsync(context.Payload.Body);
                else if (innerEnvelopeHandler != null)
                    innerEnvelopeHandler.HandleAsync(context.Payload);
                else
                    throw Guard.Exception.NotSupported("Invalid object state. Pass in CommandHandled or Envelope<CommandHandled> event handler.");
            }

            return Task.FromResult(true);
        }
    }
}
