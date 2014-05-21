using Neptuo.Commands.Interception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands.Execution
{
    public class InterceptorCollection : IDecoratedInvokeContext, ICommandHandlerAware
    {
        protected IEnumerable<IDecoratedInvoke> Interceptors { get; private set; }
        protected IEnumerator<IDecoratedInvoke> InterceptorEnumerator { get; private set; }

        public object Command { get; private set; }
        public object CommandHandler { get; private set; }

        public InterceptorCollection(IEnumerable<IDecoratedInvoke> interceptors, object commandHandler, object command)
        {
            Guard.NotNull(interceptors, "interceptors");
            Guard.NotNull(commandHandler, "commandHandler");
            Guard.NotNull(command, "command");
            Interceptors = interceptors;
            InterceptorEnumerator = interceptors.GetEnumerator();
            CommandHandler = commandHandler;
            Command = command;
        }

        public void Next()
        {
            if (InterceptorEnumerator.MoveNext())
                InterceptorEnumerator.Current.OnInvoke(this);
        }
    }
}
