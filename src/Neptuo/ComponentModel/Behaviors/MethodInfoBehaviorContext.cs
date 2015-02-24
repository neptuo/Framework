using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel.Behaviors
{
    public class MethodInfoBehaviorContext<T> : DefaultBehaviorContext<T>
    {
        public MethodInfoBehaviorContext(IEnumerable<IBehavior<T>> behaviors, T handler, MethodInfo method)
            : base(behaviors, handler)
        {
            this.SetTargetMethod(method);
        }

        public MethodInfoBehaviorContext(IEnumerable<IBehavior<T>> behaviors, T handler, MethodInfo method, int behaviorStartOffset)
            : base(behaviors, handler, behaviorStartOffset)
        {
            this.SetTargetMethod(method);
        }

        protected MethodInfoBehaviorContext(IEnumerable<IBehavior<T>> behaviors, T handler, int behaviorStartOffset)
            : base(behaviors, handler, behaviorStartOffset)
        { }

        protected override Task NextAsyncWhenNoMoreBehaviors()
        {
            MethodInfo method;
            List<object> parameters;
            if (this.TryGetTargetMethod(out method) && this.TryGetTargetParameters(out parameters))
            {
                object result = method.Invoke(Handler, parameters.ToArray());
                
                // Async method.
                Task taskResult = result as Task;
                if (taskResult != null)
                {
                    //await taskResult;
                    //this.SetTargetReturn(taskResult.Res)
                    throw new NotImplementedException();
                }

                // Has return type?
                if (typeof(void) != method.ReturnType)
                {
                    this.SetTargetReturn(result);
                    return Task.FromResult(result);
                }

                // Without any return value.
                this.SetTargetReturn(null);
                return Task.FromResult(true);
            }

            return base.NextAsyncWhenNoMoreBehaviors();
        }

        public override IBehaviorContext Clone()
        {
            return new MethodInfoBehaviorContext<T>(Behaviors.ToList(), Handler, NextBehaviorIndex - 1)
                .SetCustomValues(new KeyValueCollection(CustomValues));
        }
    }
}
