using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Features
{
    /// <summary>
    /// An implementation of <see cref="IFeatureProvider"/> which delegates features to registered objects (with concurrency support).
    /// When resolving feature, first are searched registered instances, than registered getters (<see cref="Func{T}"/>, than registered feature models and lastly search handlers.
    /// </summary>
    public class FeatureCollection : IFeatureProvider
    {
        private readonly object storageLock = new object();
        private readonly Dictionary<Type, object> features;
        private readonly Dictionary<Type, Func<object>> featureGetters;
        private readonly List<IFeatureProvider> featureModels;
        private OutFuncCollection<Type, object, bool> onSearchFeature = new OutFuncCollection<Type, object, bool>();

        /// <summary>
        /// Creates a new empty instance.
        /// </summary>
        public FeatureCollection()
        {
            features = new Dictionary<Type, object>();
            featureGetters = new Dictionary<Type, Func<object>>();
            featureModels = new List<IFeatureProvider>();
        }

        /// <summary>
        /// Creates a new instance from <paramref name="features"/>.
        /// Dictionary key is feature type and value is used a feature, it can be feature itself or <see cref="IFeatureProvider"/> or <see cref="Func{Object}"/>.
        /// </summary>
        /// <param name="features">A predefined set of features.</param>
        public FeatureCollection(IDictionary<Type, object> features)
        {
            Ensure.NotNull(features, "features");
            features = new Dictionary<Type, object>(features);
            featureGetters = new Dictionary<Type, Func<object>>();
            featureModels = new List<IFeatureProvider>();
        }

        /// <summary>
        /// Adds feature to the collection.
        /// </summary>
        /// <param name="featureType">Type of feature.</param>
        /// <param name="feature">Feature instance.</param>
        /// <returns>Self (for fluency).</returns>
        public FeatureCollection Add(Type featureType, object feature)
        {
            Ensure.NotNull(featureType, "featureType");

            lock (storageLock)
            {
                featureGetters.Remove(featureType);
                features[featureType] = feature;
            }

            return this;
        }

        /// <summary>
        /// Adds feature provider to the collection.
        /// </summary>
        /// <param name="featureType">Type of feature.</param>
        /// <param name="featureGetter">Feature provider.</param>
        /// <returns>Self (for fluency).</returns>
        public FeatureCollection AddGetter(Type featureType, Func<object> featureGetter)
        {
            Ensure.NotNull(featureType, "featureType");
            Ensure.NotNull(featureGetter, "featureGetter");

            lock (storageLock)
            {
                features.Remove(featureType);
                featureGetters[featureType] = featureGetter;
            }

            return this;
        }

        /// <summary>
        /// Adds <paramref name="container"/> as feature provider.
        /// </summary>
        /// <param name="container">A instance of feature container.</param>
        /// <returns>Self (for fluency).</returns>
        public FeatureCollection AddFeatureContainer(IFeatureProvider container)
        {
            Ensure.NotNull(container, "container");

            lock (storageLock)
                featureModels.Add(container);

            return this;
        }

        /// <summary>
        /// Registers generic handler for providing feature.
        /// <paramref name="handler"/> takes typeof requested feature 
        /// and returns <c>true</c> to indicate success; otherwise <c>false</c>.
        /// </summary>
        /// <param name="handler">Handler to register.</param>
        public void AddSearchHandler(OutFunc<Type, object, bool> handler)
        {
            Ensure.NotNull(handler, "handler");
            onSearchFeature.Add(handler);
        }

        public bool TryWith<TFeature>(out TFeature feature)
        {
            Type featureType = typeof(TFeature);
            object featureBase;
            if (features.TryGetValue(featureType, out featureBase))
            {
                feature = (TFeature)featureBase;
                return true;
            }

            Func<object> featureGetter;
            if (featureGetters.TryGetValue(featureType, out featureGetter))
            {
                feature = (TFeature)featureGetter();
                return true;
            }

            foreach (IFeatureProvider featureModel in featureModels)
            {
                if (featureModel.TryWith(out feature))
                    return true;
            }

            if (onSearchFeature.TryExecute(featureType, out featureBase))
            {
                feature = (TFeature)featureBase;
                return true;
            }

            feature = default;
            return false;
        }
    }
}
