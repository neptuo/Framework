using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.UI
{
    /// <summary>
    /// Common extensions for <see cref="IFieldViewProvider{T}"/>.
    /// </summary>
    public static class _FieldViewProviderExtensions
    {
        /// <summary>
        /// Returns field view for field and model definition.
        /// If such view is not registered, <see cref="InvalidOperationException"/> is thrown.
        /// </summary>
        /// <param name="provider">View provider.</param>
        /// <param name="modelDefinition">Model definition that contains <paramref name="fieldDefinition" /></param>
        /// <param name="fieldDefinition">Field definition to find field view for.</param>
        /// <returns>Instance of registered field view.</returns>
        /// <exception cref="InvalidOperationException">When <paramref name="provider"/> can provide field view.</exception>
        public static IFieldView<T> Get<T>(this IFieldViewProvider<T> provider, IModelDefinition modelDefinition, IFieldDefinition fieldDefinition)
        {
            Ensure.NotNull(provider, "provider");
            IFieldView<T> fieldView;
            if (provider.TryGet(modelDefinition, fieldDefinition, out fieldView))
                return fieldView;

            throw Ensure.Exception.InvalidOperation("Provider doesn't contain field view for model '{0}' and field '{1}'.", modelDefinition.Identifier, fieldDefinition.Identifier);
        }
    }
}
