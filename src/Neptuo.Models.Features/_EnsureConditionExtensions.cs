using Neptuo.Exceptions.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Features
{
    /// <summary>
    /// Feature models extensions for <see cref="Ensure.Condition"/>.
    /// </summary>
    public static class _EnsureConditionExtensions
    {
        /// <summary>
        /// Test whether <paramref name="model"/> has feature <typeparamref name="TFeature"/>.
        /// </summary>
        /// <typeparam name="TFeature">Type of required feature.</typeparam>
        /// <param name="condition">Ensure condition helper.</param>
        /// <param name="model">Model test feature on.</param>
        /// <exception cref="ArgumentOutOfRangeException">When <paramref name="model"/> doesn't have feature of type <typeparamref name="TFeature"/>.</exception>
        public static void HasFeature<TFeature>(this EnsureConditionHelper condition, IFeatureModel model)
        {
            Ensure.NotNull(condition, "condition");
            Ensure.NotNull(model, "model");

            TFeature feature;
            if (!model.TryWith(out feature))
                throw Ensure.Exception.ArgumentOutOfRange("model", "Feature of type '{0}' is required on '{1}'.", typeof(TFeature).FullName, model);
        }
    }
}
