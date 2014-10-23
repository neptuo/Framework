using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Domain
{
    /// <summary>
    /// Describes domain model.
    /// </summary>
    /// <typeparam name="TKey">The type of the domain model key.</typeparam>
    public interface IDomainModel<TKey>
        where TKey : IKey
    {
        /// <summary>
        /// The key of the model.
        /// Shouldn't be null.
        /// </summary>
        TKey Key { get; }
    }
}
