using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel.Behaviors.Processing.Reflection
{
    /// <summary>
    /// Default implementation of <see cref="IReflectionContext"/>.
    /// </summary>
    public class DefaultReflectionContext : IReflectionContext
    {
        public Type HandlerType { get; private set; }

        public DefaultReflectionContext(Type handlerType)
        {
            Ensure.NotNull(handlerType, "handlerType");
            HandlerType = handlerType;
        }
    }
}
