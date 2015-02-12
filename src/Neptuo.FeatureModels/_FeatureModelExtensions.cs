using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FeatureModels
{
    public static class _FeatureModelExtensions
    {
        /// <summary>
        /// Tries to retrieve object of type <typeparamref name="TFeature"/>.
        /// If this is not possible, throws <see cref="NotSupportedException"/>/
        /// </summary>
        /// <typeparam name="TFeature">Type of feature to retrieve.</typeparam>
        /// <param name="model">Feature model.</param>
        /// <returns>Feature of type <typeparamref name="TFeature"/>.</returns>
        /// <exception cref="NotSupportedException">If <paramref name="model"/> doesn't support feature of type <typeparamref name="TFeature"/>.</exception>
        public static TFeature With<TFeature>(this IFeatureModel model)
        {
            Guard.NotNull(model, "model");

            TFeature feature;
            if (model.TryWith(out feature))
                return feature;

            throw Guard.Exception.NotSupported(
                "Feature model '{0}' doesn't support feature '{1}'.", 
                model.GetType().FullName, 
                typeof(TFeature).FullName
            );
        }
    }
}
