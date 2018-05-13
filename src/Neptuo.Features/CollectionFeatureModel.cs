using Neptuo.Activators;
using Neptuo.Collections.Specialized;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Features
{
    /// <summary>
    /// Implementation of <see cref="IFeatureModel"/> which delegates features to registered objects (with concurrency support).
    /// When resolving feature, first are searched registered instances, than registered getters (<see cref="Func{T}"/>, than registered feature models and lastly search handlers.
    /// </summary>
    public class CollectionFeatureModel : IFeatureModel
    {
        private readonly object storageLock = new object();
        private readonly Dictionary<Type, object> features;
        private readonly Dictionary<Type, Func<object>> featureGetters;
        private readonly List<IFeatureModel> featureModels;
        private OutFuncCollection<Type, object, bool> onSearchFeature = new OutFuncCollection<Type, object, bool>();

        public CollectionFeatureModel()
        {
            features = new Dictionary<Type, object>();
            featureGetters = new Dictionary<Type, Func<object>>();
            featureModels = new List<IFeatureModel>();
        }

        public CollectionFeatureModel(IDictionary<Type, object> features)
        {
            Ensure.NotNull(features, "features");
            features = new Dictionary<Type, object>(features);
            featureGetters = new Dictionary<Type, Func<object>>();
            featureModels = new List<IFeatureModel>();
        }

        /// <summary>
        /// Adds feature to the collection.
        /// </summary>
        /// <param name="featureType">Type of feature.</param>
        /// <param name="feature">Feature instance.</param>
        /// <returns>Self (for fluency).</returns>
        public CollectionFeatureModel Add(Type featureType, object feature)
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
        public CollectionFeatureModel AddGetter(Type featureType, Func<object> featureGetter)
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

        public CollectionFeatureModel AddFeatureModel(IFeatureModel featureModel)
        {
            Ensure.NotNull(featureModel, "featureModel");
            lock (storageLock)
            {
                featureModels.Add(featureModel);
            }
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

            foreach (IFeatureModel featureModel in featureModels)
            {
                if (featureModel.TryWith(out feature))
                    return true;
            }

            if(onSearchFeature.TryExecute(featureType, out featureBase)) 
            {
                feature = (TFeature)featureBase;
                return true;
            }

            feature = default(TFeature);
            return false;   
        }
    }
}
