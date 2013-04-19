using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Constraints
{
    public class CachingConstraintProvider : IBootstrapConstraintProvider
    {
        private Dictionary<Type, IEnumerable<IBootstrapConstraint>> cache = new Dictionary<Type, IEnumerable<IBootstrapConstraint>>();
        private IBootstrapConstraintProvider provider;

        public CachingConstraintProvider(IBootstrapConstraintProvider provider)
        {
            this.provider = provider;
        }

        public IEnumerable<IBootstrapConstraint> GetConstraints(Type bootstrapTask)
        {
            if (cache.ContainsKey(bootstrapTask))
                return cache[bootstrapTask];

            IEnumerable<IBootstrapConstraint> constraints = provider.GetConstraints(bootstrapTask);
            cache[bootstrapTask] = constraints;
            return constraints;
        }

        public void ClearCache()
        {
            cache.Clear();
        }
    }
}
