using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Behaviors.Processing.Reflection
{
    /// <summary>
    /// Default implementation of <see cref="IReflectionBehaviorFactoryContext"/>.
    /// </summary>
    public class DefaultReflectionBehaviorFactoryContext : IReflectionBehaviorFactoryContext
    {
        public Type HandlerType { get; private set; }

        public DefaultReflectionBehaviorFactoryContext(Type handlerType)
        {
            Ensure.NotNull(handlerType, "handlerType");
            HandlerType = handlerType;
        }
    }
}
