using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Behaviors.Processing.Reflection
{
    /// <summary>
    /// Provides instance of behavior.
    /// </summary>
    public interface IReflectionBehaviorInstanceProvider
    {
        /// <summary>
        /// Tries to create instance of <paramref name="behaviorType"/>.
        /// </summary>
        /// <param name="context">Reflection context.</param>
        /// <param name="behaviorType">Behavior type to create instance of.</param>
        /// <returns>Instance of <paramref name="behaviorType"/>; <c>null</c> to execute next provider.</returns>
        object TryProvide(IReflectionContext context, Type behaviorType);
    }
}
