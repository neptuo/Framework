using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Features
{
    /// <summary>
    /// A feature retrieving extensions.
    /// </summary>
    public static class _FeatureProviderExtensions
    {
        /// <summary>
        /// Tries to retrieve object of type <typeparamref name="TFeature"/>.
        /// If this is not possible, throws <see cref="NotSupportedException"/>/
        /// </summary>
        /// <typeparam name="TFeature">A type of the feature to retrieve.</typeparam>
        /// <param name="provider">A feature provider.</param>
        /// <returns>Feature of type <typeparamref name="TFeature"/>.</returns>
        /// <exception cref="NotSupportedException">If <paramref name="provider"/> doesn't support feature of type <typeparamref name="TFeature"/>.</exception>
        public static TFeature With<TFeature>(this IFeatureProvider provider)
        {
            Ensure.NotNull(provider, "model");

            TFeature feature;
            if (provider.TryWith(out feature))
                return feature;

            throw Ensure.Exception.NotSupported(
                "Feature provider '{0}' doesn't support feature '{1}'.", 
                provider.GetType().FullName, 
                typeof(TFeature).FullName
            );
        }
    }
}
