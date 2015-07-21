using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Reflections.Enumerators
{
    /// <summary>
    /// Contract for enumerating types.
    /// </summary>
    public interface ITypeEnumerator
    {
        /// <summary>
        /// Current type.
        /// </summary>
        Type Current { get; }

        /// <summary>
        /// Tries to move to next type.
        /// Returns <c>true</c> when next type is awailable; <c>false</c> otherwise.
        /// </summary>
        /// <returns><c>true</c> when next type is awailable; <c>false</c> otherwise.</returns>
        bool Next();
    }
}
