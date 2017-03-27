using Neptuo;
using Neptuo.Commands.Handlers;
using Neptuo.Data;
using Neptuo.Exceptions;
using Neptuo.Formatters;
using Neptuo.Internals;
using Neptuo.Linq.Expressions;
using Neptuo.Logging;
using Neptuo.Models;
using Neptuo.Models.Keys;
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
        private HandlerDescriptorProvider descriptorProvider;
        private readonly TreeQueue queue;
        private readonly TreeQueueThreadPool threadPool;
        private readonly ICommandDistributor distributor;
        private readonly ICommandPublishingStore store;
        private readonly ISerializer formatter;
        private readonly ISchedulingProvider schedulingProvider;
        private readonly ILog log;
        private HandlerCollection handlers;

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
        /// Creates new instance with <see cref="DateTime.Now"/> as current date time provider.
        /// </summary>
        /// <param name="distributor">The command-to-the-queue distributor.</param>
        /// <param name="store">The publishing store for command persistent delivery.</param>
        /// <param name="formatter">The formatter for serializing commands.</param>
        public PersistentCommandDispatcher(ICommandDistributor distributor, ICommandPublishingStore store, ISerializer formatter)
            : this(distributor, store, formatter, new TimerSchedulingProvider(new TimerSchedulingProvider.DateTimeNowProvider()))
        { }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="distributor">The command-to-the-queue distributor.</param>
        /// <param name="store">The publishing store for command persistent delivery.</param>
        /// <param name="formatter">The formatter for serializing commands.</param>
        /// <param name="schedulingProvider">The provider of a delay computation for delayed commands.</param>
        public PersistentCommandDispatcher(ICommandDistributor distributor, ICommandPublishingStore store, ISerializer formatter, ISchedulingProvider schedulingProvider)
            : this(distributor, store, formatter, schedulingProvider, new DefaultLogFactory())
        { }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="distributor">The command-to-the-queue distributor.</param>
        /// <param name="store">The publishing store for command persistent delivery.</param>
        /// <param name="formatter">The formatter for serializing commands.</param>
        /// <param name="schedulingProvider">The provider of a delay computation for delayed commands.</param>
        public PersistentCommandDispatcher(ICommandDistributor distributor, ICommandPublishingStore store, ISerializer formatter, ISchedulingProvider schedulingProvider, ILogFactory logFactory)
        {
            Ensure.NotNull(distributor, "distributor");
            Ensure.NotNull(store, "store");
            Ensure.NotNull(formatter, "formatter");
            Ensure.NotNull(schedulingProvider, "schedulingProvider");
            Ensure.NotNull(logFactory, "logFactory");
            this.queue = new TreeQueue();
            this.threadPool = new TreeQueueThreadPool(queue);
            this.distributor = distributor;
            this.store = store;
            this.formatter = formatter;
            this.schedulingProvider = schedulingProvider;
            this.log = logFactory.Scope("PersistentCommandDispatcher");
            Initialize();
        }

        internal PersistentCommandDispatcher(TreeQueue queue, TreeQueueThreadPool threadPool, ICommandDistributor distributor, ICommandPublishingStore store, ISerializer formatter, ISchedulingProvider schedulingProvider, ILogFactory logFactory)
        {
            this.queue = queue;
            this.threadPool = threadPool;
            this.distributor = distributor;
            this.store = store;
            this.formatter = formatter;
            this.schedulingProvider = schedulingProvider;
            this.log = logFactory.Scope("PersistentCommandDispatcher");
            Initialize();
        }

        private void Initialize()
        {
            CommandExceptionHandlers = new DefaultExceptionHandlerCollection();
            DispatcherExceptionHandlers = new DefaultExceptionHandlerCollection();

            descriptorProvider = new HandlerDescriptorProvider(
                typeof(ICommandHandler<>),
                null,
                TypeHelper.MethodName<ICommandHandler<object>, object, Task>(h => h.HandleAsync),
                CommandExceptionHandlers,
                DispatcherExceptionHandlers
            );
            handlers = new HandlerCollection(log.Factory, descriptorProvider);
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

            DispatcherExceptionHandlers.Handle(new MissingCommandHandlerException(argument.ArgumentType));
            return Async.CompletedTask;
        }

        private async Task HandleInternalAsync(HandlerDescriptor handler, ArgumentDescriptor argument, object commandPayload, bool isPersistenceUsed, bool isEnvelopeDelayUsed)
        {
            try
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
                    log.Fatal("Passed in a context object.");
                    throw Ensure.Exception.NotSupported("PersistentCommandDispatcher doesn't support passing in a command handler context.");
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
                            envelope = EnvelopeFactory.Create(payload, argument.ArgumentType);
                    }

                    if (hasContextHandler)
                    {
                        log.Fatal("Context handler not supported.");
                        throw Ensure.Exception.NotSupported("PersistentCommandDispatcher doesn't support command handler context.");
                    }
                }

                if (commandWithKey == null)
                    commandWithKey = payload as ICommand;

                log.Info(commandWithKey, "Got a command.");
                log.Info(commandWithKey, "Execution on the handler '{0}'.", handler);

                // If we have command with the key, serialize it for persisten delivery.
                if (store != null && isPersistenceUsed && commandWithKey != null)
                {
                    string serializedEnvelope = await formatter.SerializeAsync(envelope);
                    store.Save(new CommandModel(commandWithKey.Key, serializedEnvelope));

                    log.Debug(commandWithKey, "Saved to the store.");
                }

                // If isEnvelopeDelayUsed, try to schedule the execution.
                // If succeeded, return.
                if (isEnvelopeDelayUsed && TrySchedule(envelope, handler, argument))
                    return;

                // Distribute the execution.
                DistributeExecution(payload, context, envelope, commandWithKey, handler);
            }
            catch (Exception e)
            {
                DispatcherExceptionHandlers.Handle(e);
            }
        }

        private bool TrySchedule(Envelope envelope, HandlerDescriptor handler, ArgumentDescriptor argument)
        {
            if (schedulingProvider.IsLaterExecutionRequired(envelope))
            {
                log.Info(envelope, "Scheduling for later execution.");

                ScheduleCommandContext context = new ScheduleCommandContext(handler, argument, envelope, OnScheduledCommand);
                schedulingProvider.Schedule(context);
                return true;
            }

            return false;
        }

        private void DistributeExecution(object payload, object context, Envelope envelope, ICommand commandWithKey, HandlerDescriptor handler)
        {
            object key = distributor.Distribute(payload);

            log.Debug(commandWithKey, "Distributing execution on the '{0}'.", key);

            queue.Enqueue(key, async () =>
            {
                log.Info(commandWithKey, "Starting execution.");

                bool isExceptionRaised = false;
                Action<Exception> additionalExceptionDecorator = e =>
                {
                    isExceptionRaised = true;

                    AggregateRootException aggregateException = e as AggregateRootException;
                    if (aggregateException != null)
                    {
                        // If envelope is created and contains source command key, use it.
                        IKey sourceCommandKey;
                        if (aggregateException.SourceCommandKey == null && envelope != null && envelope.TryGetSourceCommandKey(out sourceCommandKey))
                            aggregateException.SourceCommandKey = sourceCommandKey;

                        // If command is command with key, use it.
                        if (aggregateException.CommandKey == null && commandWithKey != null)
                            aggregateException.CommandKey = commandWithKey.Key;
                    }
                };

                try
                {
                    log.Debug(commandWithKey, "Entered try-catch.");

                    if (handler.IsContext)
                        await handler.Execute(context, additionalExceptionDecorator);
                    else if (handler.IsEnvelope)
                        await handler.Execute(envelope, additionalExceptionDecorator);
                    else if (handler.IsPlain)
                        await handler.Execute(payload, additionalExceptionDecorator);
                    else
                        throw Ensure.Exception.UndefinedHandlerType(handler);

                    log.Debug(commandWithKey, "Handler finished.");

                    // If we have command with the key, notify about successful execution.
                    if (store != null && commandWithKey != null && !isExceptionRaised)
                    {
                        await store.PublishedAsync(commandWithKey.Key);
                        log.Debug(commandWithKey, "Successfull execution saved to the store.");
                    }
                }
                catch (Exception e)
                {
                    DispatcherExceptionHandlers.Handle(e);
                    log.Fatal(commandWithKey, e.ToString());
                }

                log.Info(commandWithKey, "Execution finished.");
            });
        }

        /// <summary>
        /// Raised from the <see cref="ScheduleCommandContext.Execute"/> when scheduling provider deems.
        /// </summary>
        /// <param name="context">The context directly handler.</param>
        private void OnScheduledCommand(ScheduleCommandContext context)
        {
            // The null passed because we currently don't support contexts.
            DistributeExecution(
                context.Envelope.Body,
                null,
                context.Envelope,
                context.Envelope.Body as ICommand,
                context.Handler
            );
        }

        /// <summary>
        /// Re-publishes commands from unpublished queue.
        /// Uses <paramref name="formatter"/> to deserialize commands from store.
        /// </summary>
        /// <param name="formatter">The command deserializer.</param>
        /// <returns>The continuation task.</returns>
        public async Task RecoverAsync(IDeserializer formatter)
        {
            Ensure.NotNull(store, "store");
            Ensure.NotNull(formatter, "formatter");

            log.Debug("Starting recovery.");

            IEnumerable<CommandModel> models = await store.GetAsync();
            foreach (CommandModel model in models)
            {
                Type envelopeType = EnvelopeFactory.GetType(Type.GetType(model.CommandKey.Type));
                Envelope envelope = (Envelope)await formatter.DeserializeAsync(envelopeType, model.Payload);

                log.Debug(envelope, "Recovering an envelope.");
                await HandleAsync(envelope, false);
            }

            await store.ClearAsync();

            log.Debug("Recovery finished.");
        }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            threadPool.Dispose();
        }
    }
}
