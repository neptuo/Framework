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
    /// <summary>
    /// The implementation of <see cref="ICommandDispatcher"/> and <see cref="ICommandHandlerCollection"/> with persistent delivery.
    /// </summary>
    public class PersistentCommandDispatcher : DisposableBase, ICommandDispatcher, ICommandHandlerCollection
    {
        private readonly Dictionary<Type, HandlerDescriptor> storage = new Dictionary<Type, HandlerDescriptor>();
        private readonly TheeQueue queue = new TheeQueue();
        private readonly CommandThreadPool threadPool;
        private readonly ICommandDistributor distributor;
        private readonly ICommandPublishingStore store;
        private readonly ISerializer formatter;
        private readonly HandlerDescriptorProvider descriptorProvider;

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="distributor">The command-to-the-queue distributor.</param>
        /// <param name="store">The publishing store for command persistent delivery.</param>
        /// <param name="formatter">The formatter for serializing commands.</param>
        public PersistentCommandDispatcher(ICommandDistributor distributor, ICommandPublishingStore store, ISerializer formatter)
        {
            Ensure.NotNull(distributor, "distributor");
            Ensure.NotNull(store, "store");
            Ensure.NotNull(formatter, "formatter");
            this.distributor = distributor;
            this.store = store;
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

        private async Task HandleInternalAsync(HandlerDescriptor handler, ArgumentDescriptor argument, object commandPayload)
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
                        //TODO: Wrap reflection.
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

            // If we have command with the key, serialize it for persisten delivery.
            ICommand commandWithKey = commandPayload as ICommand;
            if (commandWithKey != null)
            {
                string serializedEnvelope = await formatter.SerializeAsync(envelope);
                store.Save(new CommandModel(commandWithKey.Key, serializedEnvelope));
            }

            // TODO: If we have the envelope and delay is used, schedule the execution...

            object key = distributor.Distribute(payload);
            queue.Enqueue(key, async () =>
            {
                if (handler.IsContext)
                    await handler.Execute(context);
                else if (handler.IsEnvelope)
                    await handler.Execute(envelope);
                else if (handler.IsPlain)
                    await handler.Execute(commandPayload);
                else
                    throw Ensure.Exception.UndefinedHandlerType(handler);

                // If we have command with the key, notify about successful execution.
                if (commandWithKey != null)
                    await store.PublishedAsync(commandWithKey.Key);
            });
        }

        /// <summary>
        /// Re-publishes events from unpublished queue.
        /// Uses <paramref name="formatter"/> to deserialize events from store.
        /// </summary>
        /// <param name="formatter">The event deserializer.</param>
        /// <returns>The continuation task.</returns>
        public async Task RecoverAsync(IDeserializer formatter)
        {
            IEnumerable<CommandModel> models = await store.GetAsync();
            foreach (CommandModel model in models)
            {
                ICommand command = (ICommand)await formatter.DeserializeAsync(Type.GetType(model.CommandKey.Type), model.Payload);
                //TODO: Skip serialization and persistence.
                await HandleAsync(command);
            }

            await store.ClearAsync();
        }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            threadPool.Dispose();
        }
    }
}
