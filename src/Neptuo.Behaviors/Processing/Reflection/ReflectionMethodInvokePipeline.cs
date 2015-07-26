using Neptuo.Activators;
using Neptuo.Behaviors.Processing.Reflection;
using Neptuo.Behaviors.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Behaviors.Processing
{
    public class ReflectionMethodInvokePipeline<THandler, TOutput> : ReflectionDefaultPipeline<THandler>
        where THandler : new()
    {
        private readonly string methodName;

        public ReflectionMethodInvokePipeline(IBehaviorProvider behaviors, IReflectionBehaviorInstanceProvider behaviorInstance, string methodName)
            : base(behaviors, behaviorInstance)
        {
            Ensure.NotNullOrEmpty(methodName, "methodName");
            this.methodName = methodName;
        }

        protected override IBehaviorContext GetBehaviorContext(IEnumerable<IBehavior<THandler>> behaviors, THandler handler)
        {
            return base.GetBehaviorContext(
                Enumerable.Concat(
                    behaviors,
                    new IBehavior<THandler>[] { new InvokeBehavior() }
                ), 
                handler
            );
        }

        public async Task<TOutput> ExecuteAsync(params object[] parameters)
        {
            Ensure.NotNull(parameters, "parameters");

            IEnumerable<IBehavior<THandler>> behaviors = GetBehaviors();
            THandler handler = HandlerFactory.Create();

            IBehaviorContext context = GetBehaviorContext(behaviors, handler)
                .SetTargetMethod(GetMethodInfo())
                .SetTargetParameters(parameters.ToList());

            await context.NextAsync();

            TOutput output;
            if (!context.TryGetTargetReturn(out output))
                output = default(TOutput);

            return output;
        }

        private MethodInfo GetMethodInfo()
        {
            return typeof(THandler).GetMethod(methodName);
        }

        private class InvokeBehavior : IBehavior<THandler>
        {
            public Task ExecuteAsync(THandler handler, IBehaviorContext context)
            {
                MethodInfo method;
                List<object> parameters;
                if (context.TryGetTargetMethod(out method) && context.TryGetTargetParameters(out parameters))
                {
                    object result = method.Invoke(handler, parameters.ToArray());

                    // Async method.
                    Task taskResult = result as Task;
                    if (taskResult != null)
                    {
                        //await taskResult;
                        //context.SetTargetReturn(taskResult.Res)
                        throw new NotImplementedException();
                    }

                    // Has return type?
                    if (typeof(void) != method.ReturnType)
                    {
                        context.SetTargetReturn(result);
                        return Task.FromResult(result);
                    }

                    // Without any return value.
                    context.SetTargetReturn(null);
                    return Task.FromResult(true);
                }

                return context.NextAsync();
            }
        }
    }
}
