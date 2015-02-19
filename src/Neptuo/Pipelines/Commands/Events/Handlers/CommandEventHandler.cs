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
        private readonly IEventHandler<CommandHandled> innerDirectHandler;
        private readonly IEventHandler<Envelope<CommandHandled>> innerEnvelopeHandler;
        private readonly IEventHandler<IEventHandlerContext<CommandHandled>> innerContextHandler;

        public CommandEventHandler(object command, IEventHandler<CommandHandled> innerHandler)
        {
            Guard.NotNull(command, "command");
            Guard.NotNull(innerHandler, "innerHandler");
            this.command = command;
            this.innerDirectHandler = innerHandler;
        }

        public CommandEventHandler(object command, IEventHandler<Envelope<CommandHandled>> innerHandler)
        {
            Guard.NotNull(command, "command");
            Guard.NotNull(innerHandler, "innerHandler");
            this.command = command;
            this.innerEnvelopeHandler = innerHandler;
        }

        public CommandEventHandler(object command, IEventHandler<IEventHandlerContext<CommandHandled>> innerHandler)
        {
            Guard.NotNull(command, "command");
            Guard.NotNull(innerHandler, "innerHandler");
            this.command = command;
            this.innerContextHandler = innerHandler;
        }

        public Task HandleAsync(IEventHandlerContext<CommandHandled> context)
        {
            if (context.Payload.Body.Command == command)
            {
                context.Registry.UnSubscribe(this);

                if (innerDirectHandler != null)
                    innerDirectHandler.HandleAsync(context.Payload.Body);
                else if (innerEnvelopeHandler != null)
                    innerEnvelopeHandler.HandleAsync(context.Payload);
                else if (innerContextHandler != null)
                    innerContextHandler.HandleAsync(context);
                else
                    throw Guard.Exception.NotSupported("Invalid object state. Pass in CommandHandled or Envelope<CommandHandled> event handler.");
            }

            return Task.FromResult(true);
        }
    }
}
