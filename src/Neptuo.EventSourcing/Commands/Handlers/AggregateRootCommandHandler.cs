using Neptuo.Activators;
using Neptuo.Models.Domains;
using Neptuo.Models.Keys;
using Neptuo.Models.Repositories;
using Neptuo.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands.Handlers
{
    /// <summary>
    /// The base command handler for aggregate root commands.
    /// </summary>
    /// <typeparam name="T">The type of the aggregate root.</typeparam>
    public abstract class AggregateRootCommandHandler<T>
        where T : AggregateRoot
    {
        private readonly IFactory<IRepository<T, IKey>> repositoryFactory;

        /// <summary>
        /// Creates new instance that uses <paramref name="repositoryFactory"/> for creating instances of repository.
        /// </summary>
        /// <param name="repositoryFactory"></param>
        public AggregateRootCommandHandler(IFactory<IRepository<T, IKey>> repositoryFactory)
        {
            Ensure.NotNull(repositoryFactory, "repositoryFactory");
            this.repositoryFactory = repositoryFactory;
        }

        /// <summary>
        /// Excutes <paramref name="handler"/> that creates new instance of aggregate.
        /// </summary>
        /// <param name="handler">The handler that creates new instance of aggregate. If returns <c>null</c>, nothing is saved.</param>
        protected Task Execute(Func<T> handler)
        {
            Ensure.NotNull(handler, "handler");

            T aggregate = handler();

            if (aggregate != null)
                repositoryFactory.Create().Save(aggregate);

            return Async.CompletedTask;
        }

        /// <summary>
        /// Loads aggregate by <paramref name="key"/> and executes <paramref name="handler"/> with it. Then the aggregate is saved.
        /// </summary>
        /// <param name="key">The key of the aggregate to load.</param>
        /// <param name="handler">The handler method for modifying aggregate.</param>
        protected Task Execute(IKey key, Action<T> handler)
        {
            Ensure.NotNull(key, "key");
            Ensure.NotNull(handler, "handler");

            IRepository<T, IKey> repository = repositoryFactory.Create();
            T aggregate = repository.Get(key);
            handler(aggregate);
            repository.Save(aggregate);

            return Async.CompletedTask;
        }
    }
}
