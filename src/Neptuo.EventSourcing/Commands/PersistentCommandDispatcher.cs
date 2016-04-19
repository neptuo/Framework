using Neptuo.Commands.Handlers;
using Neptuo.Data;
using Neptuo.Formatters;
using Neptuo.Internals;
using Neptuo.Linq.Expressions;
using Neptuo.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neptuo.Commands
{
    public class PersistentCommandDispatcher : ICommandDispatcher, ICommandHandlerCollection
    {
        private readonly Dictionary<Type, HandlerDescriptor> storage = new Dictionary<Type, HandlerDescriptor>();
        private readonly TheeQueue queue = new TheeQueue();
        private readonly CommandThreadPool threadPool;
        private readonly ICommandDistributor distributor;
        private readonly ICommandStore store;
        private readonly ICommandPublishingStore publishingStore;
        private readonly ISerializer formatter;
        private readonly HandlerDescriptorProvider descriptorProvider;

        public PersistentCommandDispatcher(ICommandDistributor distributor, ICommandStore store, ICommandPublishingStore publishingStore, ISerializer formatter)
        {
            Ensure.NotNull(distributor, "distributor");
            Ensure.NotNull(store, "store");
            Ensure.NotNull(publishingStore, "publishingStore");
            Ensure.NotNull(formatter, "formatter");
            this.distributor = distributor;
            this.store = store;
            this.publishingStore = publishingStore;
            this.formatter = formatter;
            this.threadPool = new CommandThreadPool(queue);

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
                return HandleInternalAsync(handler, argument, command);

            throw new MissingCommandHandlerException(argument.ArgumentType);
        }

        private Task HandleInternalAsync(HandlerDescriptor handler, ArgumentDescriptor argument, object commandPayload)
        {
            bool hasContextHandler = handler.IsContext;
            bool hasEnvelopeHandler = hasContextHandler || handler.IsEnvelope;

            object payload = commandPayload;
            object context = null;
            Envelope envelope = null;

            if (argument.IsContext)
            {
                // If passed argument is context, throw.
                throw Ensure.Exception.NotSupported("PersistentCommandDispatcher doesn't support passing in command handler context.");
            }
            else
            {
                // If passed argument is not context, try to create it if needed.
                if (argument.IsEnvelope)
                {
                    // If passed argument is envelope, extract payload.
                    envelope = (Envelope)payload;
                    payload = envelope.Body;
                }
                else
                {
                    // If passed argument is not envelope, try to create it if needed.
                    if (hasEnvelopeHandler)
                    {
                        MethodInfo envelopeCreateMethod = typeof(Envelope)
                            .GetMethod("Create", BindingFlags.Static | BindingFlags.Public)
                            .MakeGenericMethod(argument.ArgumentType);

                        envelope = (Envelope)envelopeCreateMethod.Invoke(null, new object[] { commandPayload });
                    }
                }

                if (hasContextHandler)
                {
                    throw Ensure.Exception.NotSupported("PersistentCommandDispatcher doesn't support command handler context.");
                }
            }

            // TODO: If we have the envelope and delay is used, schedule the execution...

            object key = distributor.Distribute(payload);
            queue.Enqueue(key, () =>
            {
                if (handler.IsContext)
                    return handler.Execute(context);
                else if (handler.IsEnvelope)
                    return handler.Execute(envelope);
                else if (handler.IsPlain)
                    return handler.Execute(commandPayload);
                else
                    throw Ensure.Exception.InvalidOperation("The handler '{0}' is of undefined type (not plain, not envelope, not context).", handler.HandlerIdentifier);
            });

            return Async.CompletedTask;
        }
    }
}
