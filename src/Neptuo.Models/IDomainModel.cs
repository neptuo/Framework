using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models
{
    /// <summary>
    /// Describes domain model.
    /// </summary>
    /// <typeparam name="TKey">A type of the domain model key.</typeparam>
    public interface IDomainModel<TKey>
        where TKey : IKey
    {
        /// <summary>
        /// Gets a key of the model (whic is never <c>null</c>).
        /// </summary>
        TKey Key { get; }
    }
}
