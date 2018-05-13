using Neptuo.Exceptions.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Features
{
    /// <summary>
    /// A feature provider extensions for <see cref="Ensure.Condition"/>.
    /// </summary>
    public static class _EnsureConditionExtensions
    {
        /// <summary>
        /// Tests whether <paramref name="provider"/> has feature <typeparamref name="TFeature"/>.
        /// </summary>
        /// <typeparam name="TFeature">A type of required feature.</typeparam>
        /// <param name="condition">An ensure condition helper.</param>
        /// <param name="provider">A provider to test feature on.</param>
        /// <exception cref="ArgumentOutOfRangeException">When <paramref name="provider"/> doesn't have feature of type <typeparamref name="TFeature"/>.</exception>
        public static void HasFeature<TFeature>(this EnsureConditionHelper condition, IFeatureProvider provider)
        {
            Ensure.NotNull(condition, "condition");
            Ensure.NotNull(provider, "model");

            TFeature feature;
            if (!provider.TryWith(out feature))
                throw Ensure.Exception.ArgumentOutOfRange("model", "Feature of type '{0}' is required on '{1}'.", typeof(TFeature).FullName, provider);
        }
    }
}
