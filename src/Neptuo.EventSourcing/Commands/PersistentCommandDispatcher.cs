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
        private readonly object timersLock = new object();

        private readonly HandlerDescriptorProvider descriptorProvider;
        private readonly TheeQueue queue = new TheeQueue();
        private readonly CommandThreadPool threadPool;
        private readonly ICommandDistributor distributor;
        private readonly ICommandPublishingStore store;
        private readonly ISerializer formatter;
        private readonly List<Tuple<Timer, ScheduleCommandContext>> timers = new List<Tuple<Timer, ScheduleCommandContext>>();
        private readonly HandlerCollection handlers;

        /// <summary>
        /// The collection of registered handlers.
        /// </summary>
        public ICommandHandlerCollection Handlers
        {
            get { return handlers; }
        }

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

            handlers = new HandlerCollection(descriptorProvider);
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
            if (handlers.TryGet(argument.ArgumentType, out handler))
                return HandleInternalAsync(handler, argument, command, isPersistenceUsed, true);

            throw new MissingCommandHandlerException(argument.ArgumentType);
        }

        private async Task HandleInternalAsync(HandlerDescriptor handler, ArgumentDescriptor argument, object commandPayload, bool isPersistenceUsed, bool isEnvelopeDelayUsed)
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

            TimeSpan delay;
            if (isEnvelopeDelayUsed && envelope.TryGetDelay(out delay))
            {
                ScheduleCommandContext scheduleContext = new ScheduleCommandContext(handler, argument, commandPayload);
                Timer timer = new Timer(
                    OnScheduledCommand, 
                    scheduleContext, 
                    delay, 
                    TimeSpan.FromMilliseconds(-1)
                );

                lock (timersLock)
                    timers.Add(new Tuple<Timer, ScheduleCommandContext>(timer, scheduleContext));

                return;
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
                        await handler.Execute(payload);
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

        private void OnScheduledCommand(object state)
        {
            ScheduleCommandContext context = (ScheduleCommandContext)state;

            lock (timersLock)
            {
                Tuple<Timer, ScheduleCommandContext> item = timers.FirstOrDefault(t => t.Item2 == context);
                if (item != null)
                    timers.Remove(item);
            }

            HandleInternalAsync(context.Handler, context.Argument, context.Payload, false, false).Wait();
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
