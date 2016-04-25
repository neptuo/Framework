using Neptuo.Commands.Handlers;
using Neptuo.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands
{
    partial class PersistentCommandDispatcher
    {
        private class HandlerCollection : ICommandHandlerCollection
        {
            private readonly Dictionary<Type, HandlerDescriptor> storage;
            private readonly HandlerDescriptorProvider descriptorProvider;

            public HandlerCollection(Dictionary<Type, HandlerDescriptor> storage, HandlerDescriptorProvider descriptorProvider)
            {
                Ensure.NotNull(storage, "storage");
                Ensure.NotNull(descriptorProvider, "descriptorProvider");
                this.storage = storage;
                this.descriptorProvider = descriptorProvider;
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
                if (storage.TryGetValue(argument.ArgumentType, out descriptor))
                {
                    handler = (ICommandHandler<TCommand>)descriptor.Handler;
                    return true;
                }

                handler = null;
                return false;
            }
        }

    }
}
