using Neptuo.Commands.Interception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands.Execution
{
    /// <summary>
    /// Context in which are interceptors executed.
    /// </summary>
    public class InterceptorExectionContext : IDecoratedInvokeContext, ICommandHandlerAware
    {
        /// <summary>
        /// List of interceptors.
        /// </summary>
        protected IEnumerable<IDecoratedInvoke> Interceptors { get; private set; }

        /// <summary>
        /// Enumerator for <see cref="Interceptors"/>.
        /// </summary>
        protected IEnumerator<IDecoratedInvoke> InterceptorEnumerator { get; private set; }

        /// <summary>
        /// Command handler to execute.
        /// </summary>
        public object CommandHandler { get; private set; }

        /// <summary>
        /// Command to handle.
        /// </summary>
        public object Command { get; private set; }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="interceptors">List of interceptors.</param>
        /// <param name="commandHandler">Command handler to execute.</param>
        /// <param name="command">Command to handle.</param>
        public InterceptorExectionContext(IEnumerable<IDecoratedInvoke> interceptors, object commandHandler, object command)
        {
            Guard.NotNull(interceptors, "interceptors");
            Guard.NotNull(commandHandler, "commandHandler");
            Guard.NotNull(command, "command");
            Interceptors = interceptors;
            InterceptorEnumerator = interceptors.GetEnumerator();
            CommandHandler = commandHandler;
            Command = command;
        }

        /// <summary>
        /// Moves to next interceptor.
        /// </summary>
        public void Next()
        {
            if (InterceptorEnumerator.MoveNext())
                InterceptorEnumerator.Current.OnInvoke(this);
        }
    }
}
