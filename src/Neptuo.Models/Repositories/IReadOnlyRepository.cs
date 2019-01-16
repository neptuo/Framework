using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Repositories
{
    /// <summary>
    /// Describes contract for getting domain models by its key (using TPL).
    /// </summary>
    /// <typeparam name="TDomainModel">A type of the domain model.</typeparam>
    /// <typeparam name="TKey">A type of the domain model key.</typeparam>
    public interface IReadOnlyRepository<TDomainModel, in TKey>
        where TKey : IKey
        where TDomainModel : IDomainModel<TKey>
    {
        /// <summary>
        /// Tries to find a model with the <paramref name="key"/>.
        /// </summary>
        /// <param name="key">A key of the model to find.</param>
        /// <returns>A continuation task containing instance of the model with the key; <c>null</c> if such model doesn't exist.</returns>
        Task<TDomainModel> FindAsync(TKey key);
    }
}
