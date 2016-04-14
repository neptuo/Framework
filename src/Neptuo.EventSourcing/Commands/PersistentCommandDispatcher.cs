using Neptuo.Commands.Handlers;
using Neptuo.Data;
using Neptuo.Formatters;
using Neptuo.Internals;
using Neptuo.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands
{
    public class PersistentCommandDispatcher : ICommandDispatcher, ICommandHandlerCollection
    {
        private readonly Dictionary<Type, HandlerDescriptor> storage = new Dictionary<Type, HandlerDescriptor>();
        private readonly ICommandStore store;
        private readonly ICommandPublishingStore publishingStore;
        private readonly ISerializer formatter;
        private readonly HandlerDescriptorProvider descriptorProvider;

        public PersistentCommandDispatcher(ICommandStore store, ICommandPublishingStore publishingStore, ISerializer formatter)
        {
            Ensure.NotNull(store, "store");
            Ensure.NotNull(publishingStore, "publishingStore");
            Ensure.NotNull(formatter, "formatter");
            this.store = store;
            this.publishingStore = publishingStore;
            this.formatter = formatter;

            this.descriptorProvider = new HandlerDescriptorProvider(
                typeof(ICommandHandler<>),
                null,
                TypeHelper.MethodName<ICommandHandler<object>, object, Task>(h => h.HandleAsync)
            );
        }

        public ICommandHandlerCollection Add<TCommand>(ICommandHandler<TCommand> handler)
        {
            Ensure.NotNull(handler, "handler");
            HandlerDescriptor descriptor = descriptorProvider.Get(handler, typeof(TCommand));
            storage[descriptor.ArgumentType] = descriptor;
            return this;
        }

        public bool TryGet<TCommand>(out ICommandHandler<TCommand> handler)
        {
            ArgumentDescriptor argument = descriptorProvider.Get(typeof(TCommand));

            HandlerDescriptor descriptor;
            if(storage.TryGetValue(argument.ArgumentType, out descriptor))
            {
                handler = (ICommandHandler<TCommand>)descriptor.Handler;
                return true;
            }

            handler = null;
            return false;
        }

        public Task HandleAsync<TCommand>(TCommand command)
        {
            Ensure.NotNull(command, "command");

            ArgumentDescriptor argument = descriptorProvider.Get(typeof(TCommand));
            HandlerDescriptor handler;
            if (storage.TryGetValue(argument.ArgumentType, out handler))
                return HandleAsyncSerial(handler, argument, command);

            throw new MissingCommandHandlerException(argument.ArgumentType);
        }

        private Task HandleAsyncSerial(HandlerDescriptor handler, ArgumentDescriptor argument, object command)
        {
            throw new NotImplementedException();
        }
    }
}
