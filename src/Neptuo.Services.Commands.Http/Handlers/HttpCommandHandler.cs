using Neptuo.Services.HttpUtilities.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands.Handlers
{
    /// <summary>
    /// Implementation of <see cref="ICommandHandler{TCommand}"/> that transfers commands over HTTP.
    /// </summary>
    /// <typeparam name="TCommand">Type of command.</typeparam>
    public class HttpCommandHandler<TCommand> : ICommandHandler<TCommand>
    {
        private readonly HttpCommandDispatcher dispatcher;

        /// <summary>
        /// Creates new instance that routes commands of type <typeparamref name="TCommand"/> to the <paramref name="route"/>.
        /// </summary>
        /// <param name="route">Route definition.</param>
        public HttpCommandHandler(RouteDefinition route)
        {
            this.dispatcher = new HttpCommandDispatcher(new DefaultRouteTable().Add(typeof(TCommand), route));
        }

        public Task HandleAsync(TCommand command)
        {
            return dispatcher.HandleAsync(command);
        }
    }
}
