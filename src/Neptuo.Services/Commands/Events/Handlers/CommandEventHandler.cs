using Neptuo.ComponentModel;
using Neptuo.Services.Events;
using Neptuo.Services.Events.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Commands.Events.Handlers
{
    public class CommandEventHandler : IEventHandler<IEventHandlerContext<CommandHandled>>
    {
        private readonly object command;
        private readonly IEventHandler<CommandHandled> innerDirectHandler;
        private readonly IEventHandler<Envelope<CommandHandled>> innerEnvelopeHandler;
        private readonly IEventHandler<IEventHandlerContext<CommandHandled>> innerContextHandler;

        public CommandEventHandler(object command, IEventHandler<CommandHandled> innerHandler)
        {
            Ensure.NotNull(command, "command");
            Ensure.NotNull(innerHandler, "innerHandler");
            this.command = command;
            this.innerDirectHandler = innerHandler;
        }

        public CommandEventHandler(object command, IEventHandler<Envelope<CommandHandled>> innerHandler)
        {
            Ensure.NotNull(command, "command");
            Ensure.NotNull(innerHandler, "innerHandler");
            this.command = command;
            this.innerEnvelopeHandler = innerHandler;
        }

        public CommandEventHandler(object command, IEventHandler<IEventHandlerContext<CommandHandled>> innerHandler)
        {
            Ensure.NotNull(command, "command");
            Ensure.NotNull(innerHandler, "innerHandler");
            this.command = command;
            this.innerContextHandler = innerHandler;
        }

        public Task HandleAsync(IEventHandlerContext<CommandHandled> context)
        {
            if (context.Payload.Body.Command == command)
            {
                context.EventHandlers.UnSubscribe(this);

                if (innerDirectHandler != null)
                    innerDirectHandler.HandleAsync(context.Payload.Body);
                else if (innerEnvelopeHandler != null)
                    innerEnvelopeHandler.HandleAsync(context.Payload);
                else if (innerContextHandler != null)
                    innerContextHandler.HandleAsync(context);
                else
                    throw Ensure.Exception.NotSupported("Invalid object state. Pass in CommandHandled or Envelope<CommandHandled> event handler.");
            }

            return Task.FromResult(true);
        }
    }
}
