using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Features
{
    /// <summary>
    /// An implementation of <see cref="IFeatureProvider"/> which tries type casting to retrive requested features.
    /// </summary>
    public class FeatureObject : IFeatureProvider
    {
        private readonly object instance;

        /// <summary>
        /// Creates a new feature wrapper for object <paramref name="instance"/>.
        /// </summary>
        /// <param name="instance">An object instance to retrieve features from.</param>
        public FeatureObject(object instance)
        {
            Ensure.NotNull(instance, "instance");
            this.instance = instance;
        }

        public bool TryWith<TFeature>(out TFeature feature)
        {
            if (instance is TFeature)
            {
                feature = (TFeature)instance;
                return true;
            }

            feature = default;
            return false;
        }
    }
}
