using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Processing.Metadata.Sorting
{
    /// <summary>
    /// Describes task dependecies.
    /// </summary>
    public interface ISortInputProvider
    {
        /// <summary>
        /// Returns types that <paramref name="type"/> depends on.
        /// </summary>
        /// <param name="type">Type to return dependencies of.</param>
        /// <returns>Enumeration of types that <paramref name="type"/> depends on.</returns>
        IEnumerable<Type> GetInputs(Type type);
    }
}