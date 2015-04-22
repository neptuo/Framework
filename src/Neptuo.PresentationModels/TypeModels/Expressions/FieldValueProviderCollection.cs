using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels.Expressions
{
    /// <summary>
    /// Default implementation of <see cref="IFieldValueProviderCollection{TModel}"/>
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class FieldValueProviderCollection<TModel> : IFieldValueProviderCollection<TModel>
    {
        private readonly Dictionary<string, IFieldValueProvider<TModel>> storage = new Dictionary<string, IFieldValueProvider<TModel>>();

        /// <summary>
        /// Registers <paramref name="provider"/> to be used for field with identifier <paramref name="fieldIdentifier"/>.
        /// </summary>
        /// <param name="fieldIdentifier">Field identifier.</param>
        /// <param name="provider">Value provider.</param>
        /// <returns>Self (for fluency).</returns>
        public FieldValueProviderCollection<TModel> Add(string fieldIdentifier, IFieldValueProvider<TModel> provider)
        {
            Ensure.NotNullOrEmpty(fieldIdentifier, "fieldIdentifier");
            Ensure.NotNull(provider, "provider");
            storage[fieldIdentifier] = provider;
            return this;
        }

        public bool TryGet(string fieldIdentifier, out IFieldValueProvider<TModel> provider)
        {
            Ensure.NotNullOrEmpty(fieldIdentifier, "fieldIdentifier");
            return storage.TryGetValue(fieldIdentifier, out provider);
        }
    }
}
