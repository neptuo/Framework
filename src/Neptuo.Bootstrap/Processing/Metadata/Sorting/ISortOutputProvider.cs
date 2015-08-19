using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Processing.Metadata.Sorting
{
    /// <summary>
    /// Describes task products.
    /// </summary>
    public interface ISortOutputProvider
    {
        /// <summary>
        /// Returns types that are produced by <paramref name="type" />.
        /// </summary>
        /// <param name="type">Type to return products of.</param>
        /// <returns>Enumeration of types that are produced by <paramref name="type"/>.</returns>
        IEnumerable<Type> GetOutputs(Type type);
    }
}
