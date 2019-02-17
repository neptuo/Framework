using Neptuo.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands.Handlers
{
    /// <summary>
    /// An implementation of <see cref="ICommandHandler{TCommand}"/> that transfers commands over HTTP.
    /// </summary>
    /// <typeparam name="TCommand">A type of the command.</typeparam>
    public class HttpCommandHandler<TCommand> : ICommandHandler<TCommand>
    {
        private readonly HttpCommandDispatcher dispatcher;

        /// <summary>
        /// Creates new instance that sends commands of type <typeparamref name="TCommand"/> throught <paramref name="objectSender"/>.
        /// </summary>
        /// <param name="objectSender">An object sender.</param>
        public HttpCommandHandler(ObjectSender objectSender)
        {
            dispatcher = new HttpCommandDispatcher(objectSender);
        }

        public Task HandleAsync(TCommand command) => dispatcher.HandleAsync(command);
    }
}
