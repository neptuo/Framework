using Neptuo;
using Neptuo.Activators;
using Neptuo.Commands.Handlers;
using Neptuo.Models.Domains;
using Neptuo.Models.Keys;
using Neptuo.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands
{
    /// <summary>
    /// A common extensions for <see cref="ICommandHandlerCollection"/>
    /// </summary>
    public static class _CommandHandlerCollectionExtensions
    {
        /// <summary>
        /// Registers <paramref name="handler"/> to handle commands of type <typeparamref name="TCommand"/> on aggregate <typeparamref name="TAggregate"/>.
        /// </summary>
        /// <typeparam name="TAggregate">A type of the aggregate.</typeparam>
        /// <typeparam name="TCommand">A type of the command to handle.</typeparam>
        /// <param name="handlers">A collection of handlers.</param>
        /// <param name="repositoryFactory">A factory for repository.</param>
        /// <param name="aggregateKeyGetter">A function to get a key of the aggregate from a command.</param>
        /// <param name="handler">A command handling functions.</param>
        /// <returns><paramref name="handlers"/> for fluency.</returns>
        public static ICommandHandlerCollection Add<TAggregate, TCommand>(this ICommandHandlerCollection handlers, IFactory<IRepository<TAggregate, IKey>> repositoryFactory, Func<TCommand, IKey> aggregateKeyGetter, Action<TAggregate, TCommand> handler)
            where TAggregate : AggregateRoot
            where TCommand : ICommand
        {
            Ensure.NotNull(handlers, "handlers");
            handlers.Add(new DelegateCommandHandler<TAggregate, TCommand>(repositoryFactory, aggregateKeyGetter, handler));
            return handlers;
        }

        private class DelegateCommandHandler<TAggregate, TCommand> : AggregateRootCommandHandler<TAggregate>, ICommandHandler<TCommand>
            where TAggregate : AggregateRoot
            where TCommand : ICommand
        {
            private readonly Func<TCommand, IKey> aggregateKeyGetter;
            private readonly Action<TAggregate, TCommand> handler;

            public DelegateCommandHandler(IFactory<IRepository<TAggregate, IKey>> repositoryFactory, Func<TCommand, IKey> aggregateKeyGetter, Action<TAggregate, TCommand> handler)
                : base(repositoryFactory)
            {
                Ensure.NotNull(aggregateKeyGetter, "aggregateKeyGetter");
                Ensure.NotNull(handler, "handler");
                this.aggregateKeyGetter = aggregateKeyGetter;
                this.handler = handler;
            }

            public Task HandleAsync(TCommand command)
            {
                return Execute(aggregateKeyGetter(command), aggregate => handler(aggregate, command));
            }
        }
    }
}
