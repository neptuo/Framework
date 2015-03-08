using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FeatureModels
{
    /// <summary>
    /// Implementation of <see cref="IFeatureModel"/> which tries type casting to retrive requested feature.
    /// </summary>
    public class ObjectFeatureModel : IFeatureModel
    {
        private readonly object instance;

        /// <summary>
        /// Creates feature model for object <paramref name="instance"/>.
        /// </summary>
        /// <param name="instance">Object instance to retrieve feature from.</param>
        public ObjectFeatureModel(object instance)
        {
            Ensure.NotNull(instance, "instance");
            this.instance = instance;
        }

        public bool TryWith<TFeature>(out TFeature feature)
        {
            if(instance is TFeature)
            {
                feature = (TFeature)instance;
                return true;
            }

            feature = default(TFeature);
            return false;
        }
    }
}
