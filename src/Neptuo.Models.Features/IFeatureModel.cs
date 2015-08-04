using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Features
{
    /// <summary>
    /// Describes extensible model.
    /// </summary>
    public interface IFeatureModel
    {
        /// <summary>
        /// Tries to retrieve object of type <typeparamref name="TFeature"/>.
        /// If this is possible, returns <c>true</c>; otherwise <c>false</c>.
        /// </summary>
        /// <typeparam name="TFeature">Type of feature to retrieve.</typeparam>
        /// <param name="feature">Output instance of feature; <c>null</c> if not supported.</param>
        /// <returns><c>true</c> if feature of type <typeparamref name="TFeature"/> is supported; otherwise <c>false</c>.</returns>
        bool TryWith<TFeature>(out TFeature feature);
    }
}
