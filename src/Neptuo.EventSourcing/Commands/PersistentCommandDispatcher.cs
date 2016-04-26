using Neptuo.Commands.Handlers;
using Neptuo.Data;
using Neptuo.Exceptions;
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
    public partial class PersistentCommandDispatcher : DisposableBase, ICommandDispatcher
    {
        private readonly Dictionary<Type, HandlerDescriptor> storage = new Dictionary<Type, HandlerDescriptor>();
        private readonly HandlerDescriptorProvider descriptorProvider;
        private readonly TheeQueue queue = new TheeQueue();
        private readonly CommandThreadPool threadPool;
        private readonly ICommandDistributor distributor;
        private readonly ICommandPublishingStore store;
        private readonly ISerializer formatter;

        /// <summary>
        /// The collection of registered handlers.
        /// </summary>
        public ICommandHandlerCollection Handlers { get; private set; }

        /// <summary>
        /// The collection of exception handlers for exception from the command processing.
        /// </summary>
        public IExceptionHandlerCollection CommandExceptionHandlers { get; set; }

        /// <summary>
        /// The collection of exception handlers for exception from the infrastructure.
        /// </summary>
        public IExceptionHandlerCollection DispatcherExceptionHandlers { get; set; }

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

            CommandExceptionHandlers = new DefaultExceptionHandlerCollection();
            DispatcherExceptionHandlers = new DefaultExceptionHandlerCollection();

            this.descriptorProvider = new HandlerDescriptorProvider(
                typeof(ICommandHandler<>),
                null,
                TypeHelper.MethodName<ICommandHandler<object>, object, Task>(h => h.HandleAsync),
                CommandExceptionHandlers,
                DispatcherExceptionHandlers
            );

            Handlers = new HandlerCollection(storage, descriptorProvider);
        }

        public Task HandleAsync<TCommand>(TCommand command)
        {
            return HandleAsync<TCommand>(command, true);
        }

        private Task HandleAsync<TCommand>(TCommand command, bool isPersistenceUsed)
        {
            Ensure.NotNull(command, "command");

            ArgumentDescriptor argument = descriptorProvider.Get(command);
            HandlerDescriptor handler;
            if (storage.TryGetValue(argument.ArgumentType, out handler))
                return HandleInternalAsync(handler, argument, command, isPersistenceUsed);

            throw new MissingCommandHandlerException(argument.ArgumentType);
        }

        private async Task HandleInternalAsync(HandlerDescriptor handler, ArgumentDescriptor argument, object commandPayload, bool isPersistenceUsed)
        {
            bool hasContextHandler = handler.IsContext;
            bool hasEnvelopeHandler = hasContextHandler || handler.IsEnvelope;

            object payload = commandPayload;
            object context = null;
            Envelope envelope = null;

            ICommand commandWithKey = null;
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
                    commandWithKey = payload as ICommand;
                    hasEnvelopeHandler = hasEnvelopeHandler || commandWithKey != null;

                    // If passed argument is not envelope, try to create it if needed.
                    if (hasEnvelopeHandler)
                    {
                        //TODO: Wrap reflection.
                        MethodInfo envelopeCreateMethod = typeof(Envelope)
                            .GetMethods(BindingFlags.Static | BindingFlags.Public)
                            .First(m => m.Name == "Create" && m.IsGenericMethod)
                            .MakeGenericMethod(argument.ArgumentType);

                        envelope = (Envelope)envelopeCreateMethod.Invoke(null, new object[] { payload });
                    }
                }

                if (hasContextHandler)
                {
                    throw Ensure.Exception.NotSupported("PersistentCommandDispatcher doesn't support command handler context.");
                }
            }

            if (commandWithKey == null)
                commandWithKey = payload as ICommand;

            // If we have command with the key, serialize it for persisten delivery.
            if (isPersistenceUsed && commandWithKey != null)
            {
                string serializedEnvelope = await formatter.SerializeAsync(envelope);
                store.Save(new CommandModel(commandWithKey.Key, serializedEnvelope));
            }

            // TODO: If we have the envelope and delay is used, schedule the execution...
            TimeSpan delay;
            if (envelope.TryGetDelay(out delay))
            {

            }

            object key = distributor.Distribute(payload);
            queue.Enqueue(key, async () =>
            {
                try
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
                }
                catch (Exception e)
                {
                    DispatcherExceptionHandlers.Handle(e);
                }
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
                Type envelopeType = typeof(Envelope<>).MakeGenericType(Type.GetType(model.CommandKey.Type));
                Envelope envelope = (Envelope)await formatter.DeserializeAsync(envelopeType, model.Payload);
                await HandleAsync(envelope, false);
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
