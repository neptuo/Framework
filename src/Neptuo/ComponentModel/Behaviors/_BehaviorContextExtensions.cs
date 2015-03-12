using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel.Behaviors
{
    /// <summary>
    /// Extensions for custom values in <see cref="IBehaviorContext"/>.
    /// </summary>
    public static class _BehaviorContextExtensions
    {
        #region TargetMethod

        public static bool TryGetTargetMethod(this IBehaviorContext context, out MethodInfo method)
        {
            Ensure.NotNull(context, "context");
            return context.CustomValues.TryGet("TargetMethod", out method);
        }

        public static IBehaviorContext SetTargetMethod(this IBehaviorContext context, MethodInfo method)
        {
            Ensure.NotNull(context, "context");
            context.CustomValues.Set("TargetMethod", method);
            return context;
        }

        #endregion

        #region TargetParameters

        public static bool TryGetTargetParameters(this IBehaviorContext context, out List<object> parameters)
        {
            Ensure.NotNull(context, "context");
            return context.CustomValues.TryGet("TargetParameters", out parameters);
        }

        public static IBehaviorContext SetTargetParameters(this IBehaviorContext context, List<object> parameters)
        {
            Ensure.NotNull(context, "context");
            context.CustomValues.Set("TargetParameters", parameters);
            return context;
        }

        #endregion

        #region TargetReturn

        public static bool TryGetTargetReturn<TOutput>(this IBehaviorContext context, out TOutput output)
        {
            Ensure.NotNull(context, "context");
            return context.CustomValues.TryGet("TargetReturn", out output);
        }

        public static IBehaviorContext SetTargetReturn(this IBehaviorContext context, object output)
        {
            Ensure.NotNull(context, "context");
            context.CustomValues.Set("TargetReturn", output);
            return context;
        }

        #endregion
    }
}
