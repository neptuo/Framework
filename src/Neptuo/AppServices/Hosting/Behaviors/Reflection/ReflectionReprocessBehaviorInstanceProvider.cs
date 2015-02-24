using Neptuo.AppServices.Behaviors;
using Neptuo.AppServices.Behaviors.Hosting;
using Neptuo.ComponentModel.Behaviors.Processing.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.AppServices.Hosting.Behaviors.Reflection
{
    /// <summary>
    /// Instance provider for <see cref="ReprocessBehavior"/> and <see cref="ReprocessAttribute"/>.
    /// </summary>
    public class ReflectionReprocessBehaviorInstanceProvider : IReflectionBehaviorInstanceProvider
    {
        public object TryProvide(IReflectionContext context, Type behaviorType)
        {
            if (behaviorType != typeof(ReprocessBehavior))
                return null;

            ReprocessAttribute attribute = context.HandlerType.GetCustomAttribute<ReprocessAttribute>();
            if (attribute == null)
                return null;

            TimeSpan delay = attribute.DelayBeforeReprocess;
            if (delay != TimeSpan.Zero)
                return new ReprocessBehavior(attribute.Count, delay);

            return new ReprocessBehavior(attribute.Count);
        }
    }
}
