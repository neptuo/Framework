using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    /// <summary>
    /// Builder for metadata.
    /// </summary>
    public interface IMetadataBuilder : IReadOnlyKeyValueCollection
    {
        /// <summary>
        /// Sets <paramref name="identifier"/> to <paramref name="value"/>.
        /// </summary>
        /// <param name="identifier">Metadata key.</param>
        /// <param name="value">Value to associate with <paramref name="identifier"/>.</param>
        /// <returns>Self (for fluency).</returns>
        IMetadataBuilder Add(string identifier, object value);
    }
}
