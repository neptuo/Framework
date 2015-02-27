using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel.Behaviors.Providers
{
    /// <summary>
    /// Behavior provider based on attribute decoration on handler type.
    /// </summary>
    public class AttributeBehaviorProvider : MappingBehaviorProviderBase
    {
        public AttributeBehaviorProvider AddMapping(Type behaviorContract, Type behaviorImplementation)
        {
            AddMappingInternal(behaviorContract, behaviorImplementation);
            return this;
        }

        protected override IEnumerable<Type> FindBehaviors(Type handlerType)
        {
            return handlerType.GetCustomAttributes(true).Select(a => a.GetType());
        }
    }
}
