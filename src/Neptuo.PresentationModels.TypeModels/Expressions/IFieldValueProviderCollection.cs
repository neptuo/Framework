using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels.Expressions
{
    /// <summary>
    /// Collection of <see cref="IFieldValueProviderCollection{TModel}"/>.
    /// </summary>
    /// <typeparam name="TModel">Type of model.</typeparam>
    public interface IFieldValueProviderCollection<TModel>
    {
        /// <summary>
        /// Tries to get value provider for field with identifier <paramref name="fieldIdentifier"/>.
        /// </summary>
        /// <param name="fieldIdentifier">Field identifier.</param>
        /// <param name="provider">Value provider.</param>
        /// <returns><c>true</c> if provider was found; <c>false</c> otherwise.</returns>
        bool TryGet(string fieldIdentifier, out IFieldValueProvider<TModel> provider);
    }
}
