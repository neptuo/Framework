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
    public class AttributeBehaviorProvider : MappingBehaviorProviderBase
    {
        public AttributeBehaviorProvider AddMapping(Type behaviorAttribute, Type behaviorImplementation)
        {
            InsertOrUpdateMappingInternal(null, behaviorAttribute, behaviorImplementation);
            return this;
        }

        public AttributeBehaviorProvider InsertMapping(int index, Type behaviorAttribute, Type behaviorImplementation)
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
