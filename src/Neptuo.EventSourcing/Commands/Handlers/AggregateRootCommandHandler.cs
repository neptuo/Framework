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
    /// Base command handler for aggregate root commands.
    /// </summary>
    /// <typeparam name="T">The type of the aggregate root.</typeparam>
    public abstract class AggregateRootCommandHandler<T>
        where T : AggregateRoot
    {
        private readonly IFactory<IRepository<T, IKey>> repositoryFactory;

        public AggregateRootCommandHandler(IFactory<IRepository<T, IKey>> repositoryFactory)
        {
            Ensure.NotNull(repositoryFactory, "repositoryFactory");
            this.repositoryFactory = repositoryFactory;
        }

        /// <summary>
        /// Spustí <paramref name="handler"/>, který vytváří novou instanci agregátu.
        /// </summary>
        /// <param name="handler">Metoda, která vytváří novou instanci agregátu.</param>
        protected Task Execute(Func<T> handler)
        {
            Ensure.NotNull(handler, "handler");

            T aggregate = handler();
            repositoryFactory.Create().Save(aggregate);

            return Async.CompletedTask;
        }

        /// <summary>
        /// Vytáhne istanci agregátu podle <paramref name="key"/> a spustí nad ní <paramref name="handler"/>. Následně agregát uloží.
        /// </summary>
        /// <param name="key">Klíč agregátu.</param>
        /// <param name="handler">Metoda modifikující agregát.</param>
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
