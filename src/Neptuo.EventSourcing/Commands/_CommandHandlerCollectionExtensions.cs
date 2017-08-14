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
        public ICommandHandlerCollection Add<TAggregate, TCommand>(this ICommandHandlerCollection handlers, IFactory<IRepository<TAggregate, IKey>> repositoryFactory, Action<TAggregate, TCommand> handler)
            where TAggregate : AggregateRoot
            where TCommand : IAggregateCommand
        {
            Ensure.NotNull(handlers, "handlers");
            handlers.Add(new DelegateCommandHandler<TAggregate, TCommand>(repository, command => command.AggregateKey, handler));
            return handlers;
        }

        public ICommandHandlerCollection Add<TAggregate, TCommand>(this ICommandHandlerCollection handlers, IFactory<IRepository<TAggregate, IKey>> repositoryFactory, Func<TCommand, IKey> aggregateKeyGetter, Action<TAggregate, TCommand> handler)
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
