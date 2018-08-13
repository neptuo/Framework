using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Repositories
{
    /// <summary>
    /// Describes contract for getting and storing domain models by its key (using TPL).
    /// </summary>
    /// <typeparam name="TDomainModel">A type of the domain model.</typeparam>
    /// <typeparam name="TKey">A type of the domain model key.</typeparam>
    public interface IAsyncRepository<TDomainModel, in TKey> : IReadOnlyAsyncRepository<TDomainModel, TKey>
        where TKey : IKey
        where TDomainModel : IDomainModel<TKey>
    {
        /// <summary>
        /// Saves changes to the <paramref name="model"/> to the underlaying storage.
        /// </summary>
        /// <param name="model">An instance of the model to save.</param>
        /// <returns>A continuation task.</returns>
        Task SaveAsync(TDomainModel model);
    }
}
