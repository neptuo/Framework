using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Repositories
{
    /// <summary>
    /// The extensions for <see cref="IReadOnlyRepository{TDomainModel, TKey}"/>.
    /// </summary>
    public static class _RepositoryExtensions
    {
        /// <summary>
        /// Returns instance of type <typeparamref name="TDomainModel"/> associated with the <paramref name="key"/> in the <paramref name="repository"/>.
        /// If no such instance exists, throws <see cref="AggregateRootNotFoundException"/>.
        /// </summary>
        /// <typeparam name="TDomainModel">The type of the model to return.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="key">The key to model associated with.</param>
        /// <returns>The instance of model associated with the <paramref name="key"/>.</returns>
        /// <exception cref="AggregateRootNotFoundException">When no such model is associated with the <paramref name="key"/>.</exception>
        public static async Task<TDomainModel> GetAsync<TDomainModel>(this IReadOnlyRepository<TDomainModel, IKey> repository, IKey key)
            where TDomainModel : AggregateRoot
        {
            Ensure.NotNull(repository, "repository");

            TDomainModel model = await repository.FindAsync(key);
            if (model == null)
                throw new AggregateRootNotFoundException(key);

            return model;
        }
    }
}
