using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Collections.Specialized
{
    /// <summary>
    /// Readonly version of keyed collection.
    /// </summary>
    public interface IReadOnlyKeyValueCollection
    {
        /// <summary>
        /// Tries to get value associated with <paramref name="key"/>.
        /// </summary>
        /// <param name="key">Key which value should be returned.</param>
        /// <param name="value">Output value associted with <paramref name="key"/>.</param>
        /// <returns><c>true</c> if collection contains value with <paramref name="key"/> as key; <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="key"/> is <c>null</c>.</exception>
        bool TryGet(string key, out object value);
    }
}
