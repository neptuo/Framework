using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    /// <summary>
    /// Collection of meta data key-value pairs.
    /// </summary>
    public interface IModelMetadataCollection
    {
        /// <summary>
        /// Tries to get value associated with <paramref name="key"/>.
        /// Returns <c>true</c> if value was provided; false otherwise.
        /// </summary>
        /// <param name="key">Meta data key.</param>
        /// <param name="value">Associated value.</param>
        /// <returns><c>true</c> if value was provided; false otherwise.</returns>
        bool TryGet(string key, out object value);
    }
}
