using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.UI
{
    /// <summary>
    /// Common extensions for <see cref="IModelViewProvider{T}"/>.
    /// </summary>
    public static class _ModelViewProviderExtensions
    {
        /// <summary>
        /// Returns model view for model definition.
        /// If such view is not registered, <see cref="InvalidOperationException"/> is thrown.
        /// </summary>
        /// <param name="provider">View provider.</param>
        /// <param name="modelDefinition">Model definition.</param>
        /// <returns>Instance of registered model view.</returns>
        /// <exception cref="InvalidOperationException">When <paramref name="provider"/> can provide model view.</exception>
        public static IModelView<T> Get<T>(this IModelViewProvider<T> provider, IModelDefinition modelDefinition)
        {
            Ensure.NotNull(provider, "provider");
            IModelView<T> modelView;
            if (provider.TryGet(modelDefinition, out modelView))
                return modelView;

            throw Ensure.Exception.InvalidOperation("Provider doesn't contain model view for model '{0}'.", modelDefinition.Identifier);
        }
    }
}
