using Neptuo.Activators;
using Neptuo.ComponentModel;
using Neptuo.Services.Commands.Events;
using Neptuo.Services.Commands.Execution;
using Neptuo.Services.Commands.Handlers;
using Neptuo.Services.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Commands
{
    /// <summary>
    /// Command dispatcher based on registration from <see cref="IDependencyProvider"/>.
    /// </summary>
    public class DependencyCommandDispatcher : ICommandDispatcher
    {
        private IDependencyProvider dependencyProvider;
        private IEventDispatcher eventDispatcher;

        /// <summary>
        /// Initializes new instance with <paramref name="dependencyProvider"/>.
        /// </summary>
        /// <param name="dependencyProvider">Source for registrations.</param>
        public DependencyCommandDispatcher(IDependencyProvider dependencyProvider, IEventDispatcher eventDispatcher)
        {
            Ensure.NotNull(dependencyProvider, "dependencyProvider");
            Ensure.NotNull(eventDispatcher, "eventDispatcher");
            this.dependencyProvider = dependencyProvider;
            this.eventDispatcher = eventDispatcher;
        }

        /// <summary>
        /// Handles <paramref name="command"/>.
        /// </summary>
        /// <param name="command">Command to handle.</param>
        public Task HandleAsync(object command)
        {
            Ensure.NotNull(command, "command");
            return HandleInternal(command, true);
        }

        protected virtual Task HandleInternal(object command, bool handleException)
        {
            ICommandExecutor executor = null;
            try
            {
                ICommandExecutorFactory executorFactory = dependencyProvider.Resolve<ICommandExecutorFactory>();
                executor = executorFactory.CreateExecutor(command);
                executor.OnCommandHandled += OnCommandHandled;
                executor.Handle(command);
            }
            catch (Exception e)
            {
                if (handleException)
                {
                    HandleException(e);
                    return Task.FromResult(false);
                }

                Exception commandException = command as Exception;
                if (commandException != null)
                    throw new CommandDispatcherException("Unahandled exception during command execution.", commandException);

                throw new CommandDispatcherException("Unahandled exception during command execution.", e);
            }
            finally
            {
                IDisposable disposable = executor as IDisposable;
                if (disposable != null)
                    disposable.Dispose();
            }

            return Task.FromResult(true);
        }

        private void OnCommandHandled(ICommandExecutor executor, object command)
        {
            executor.OnCommandHandled -= OnCommandHandled;
            ICommand guidCommand = command as ICommand;
            Envelope<CommandHandled> envelope;
            if (guidCommand != null)
                envelope = new Envelope<CommandHandled>(new CommandHandled(guidCommand), guidCommand.Guid);
            else
                envelope = Envelope.Create(new CommandHandled(command));

            eventDispatcher.PublishAsync(envelope);
        }

        /// <summary>
        /// Handles exceptions occured while handling or validating command.
        /// </summary>
        /// <param name="exception">Exception that occured.</param>
        protected virtual void HandleException(Exception exception)
        {
            Ensure.NotNull(exception, "exception");
            HandleInternal(exception, false);
        }
    }
}
