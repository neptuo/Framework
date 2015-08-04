using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Behaviors.Providers
{
    /// <summary>
    /// Behavior provider based on attribute decoration on handler type.
    /// </summary>
    public class AttributeBehaviorCollection : MappingBehaviorProviderBase
    {
        /// <summary>
        /// Adds mapping with <paramref name="behaviorAttribute"/> as attribute and <paramref name="behaviorImplementation"/> as implementation type.
        /// </summary>
        /// <param name="behaviorAttribute">Behavior attribute.</param>
        /// <param name="behaviorImplementation">Behavior contract implementor.</param>
        /// <returns>Self (for fluency).</returns>
        public AttributeBehaviorCollection Add(Type behaviorAttribute, Type behaviorImplementation)
        {
            InsertOrUpdateMappingInternal(null, behaviorAttribute, behaviorImplementation);
            return this;
        }

        /// <summary>
        /// Adds mapping with <paramref name="behaviorAttribute"/> as attribute and <paramref name="behaviorImplementation"/> as implementation type.
        /// </summary>
        /// <param name="index">Index at which to insert behavior mapping.</param>
        /// <param name="behaviorAttribute">Behavior attribute.</param>
        /// <param name="behaviorImplementation">Behavior contract implementor.</param>
        /// <returns>Self (for fluency).</returns>
        public AttributeBehaviorCollection Insert(int index, Type behaviorAttribute, Type behaviorImplementation)
        {
            InsertOrUpdateMappingInternal(index, behaviorAttribute, behaviorImplementation);
            return this;
        }

        protected override IEnumerable<Type> FindBehaviorContracts(Type handlerType)
        {
            return handlerType.GetCustomAttributes(true).Select(a => a.GetType());
        }
    }
}
