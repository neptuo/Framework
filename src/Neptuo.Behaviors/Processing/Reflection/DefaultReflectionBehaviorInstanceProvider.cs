using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Behaviors.Processing.Reflection
{
    /// <summary>
    /// Default implementation of <see cref="IReflectionBehaviorInstanceProvider"/> which uses <see cref="Activator.CreateInstance"/>.
    /// </summary>
    public class DefaultReflectionBehaviorInstanceProvider : IReflectionBehaviorInstanceProvider
    {
        public object TryProvide(IReflectionContext context, Type behaviorType)
        {
            return Activator.CreateInstance(behaviorType);
        }
    }
}
