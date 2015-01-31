using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FeatureModels
{
    /// <summary>
    /// Implementation of <see cref="IFeatureModel"/> which delegates features to registered objects (with concurrent support.).
    /// </summary>
    public class MappingFeatureModel : IFeatureModel
    {
        private readonly IDictionary<Type, object> features;

        public MappingFeatureModel(bool isSingleThread)
        {
            if (isSingleThread)
                features = new Dictionary<Type, object>();
            else
                features = new ConcurrentDictionary<Type, object>();
        }

        public MappingFeatureModel(bool isSingleThread, IDictionary<Type, object> features)
        {
            Guard.NotNull(features, "features");
            if (isSingleThread)
                this.features = new Dictionary<Type, object>(features);
            else
                this.features = new ConcurrentDictionary<Type, object>(features);
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

            feature = default(TFeature);
            return false;   
        }
    }
}
