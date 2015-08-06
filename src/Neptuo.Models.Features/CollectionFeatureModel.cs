using Neptuo.Activators;
using Neptuo.ComponentModel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Features
{
    /// <summary>
    /// Implementation of <see cref="IFeatureModel"/> which delegates features to registered objects (with concurrency support).
    /// When resolving feature, first are searched registered instances, than registered getters (<see cref="Func{T}"/>, 
    /// than registered factories (<see cref="IActivator{T}"/>) and lastly search handlers.
    /// </summary>
    public class CollectionFeatureModel : IFeatureModel
    {
        private readonly IDictionary<Type, object> features;
        private readonly IDictionary<Type, Func<object>> featureGetters;
        private readonly IDictionary<Type, IActivator<object>> featureFactories;
        private OutFuncCollection<Type, object, bool> onSearchFeature = new OutFuncCollection<Type, object, bool>();

        public CollectionFeatureModel(bool isSingleThread)
        {
            if (isSingleThread)
            {
                features = new Dictionary<Type, object>();
                featureGetters = new Dictionary<Type, Func<object>>();
                featureFactories = new Dictionary<Type, IActivator<object>>();
            }
            else
            {
                features = new ConcurrentDictionary<Type, object>();
                featureGetters = new ConcurrentDictionary<Type, Func<object>>();
                featureFactories = new ConcurrentDictionary<Type, IActivator<object>>();
            }
        }

        public CollectionFeatureModel(bool isSingleThread, IDictionary<Type, object> features)
        {
            Ensure.NotNull(features, "features");
            if (isSingleThread)
            {
                features = new Dictionary<Type, object>(features);
                featureGetters = new Dictionary<Type, Func<object>>();
                featureFactories = new Dictionary<Type, IActivator<object>>();
            }
            else
            {
                features = new ConcurrentDictionary<Type, object>(features);
                featureGetters = new ConcurrentDictionary<Type, Func<object>>();
                featureFactories = new ConcurrentDictionary<Type, IActivator<object>>();
            }
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
            featureGetters.Remove(featureType);
            featureFactories.Remove(featureType);
            features[featureType] = feature;
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
            features.Remove(featureType);
            featureFactories.Remove(featureType);
            featureGetters[featureType] = featureGetter;
            return this;
        }

        /// <summary>
        /// Adds feature factory to the collection.
        /// </summary>
        /// <param name="featureType">Type of feature.</param>
        /// <param name="featureFactory">Feature factory.</param>
        /// <returns>Self (for fluency).</returns>
        public CollectionFeatureModel AddFactory(Type featureType, IActivator<object> featureFactory)
        {
            Ensure.NotNull(featureType, "featureType");
            Ensure.NotNull(featureFactory, "featureFactory");
            features.Remove(featureType);
            featureGetters.Remove(featureType);
            featureFactories[featureType] = featureFactory;
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

            IActivator<object> featureFactory;
            if (featureFactories.TryGetValue(featureType, out featureFactory))
            {
                feature = (TFeature)featureFactory.Create();
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
