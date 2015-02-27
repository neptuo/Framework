﻿using Neptuo.ComponentModel.Behaviors.Processing.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel.Behaviors.Processing
{
    public class MethodInvokePipeline<THandler, TOutput> : DefaultPipeline<THandler>
        where THandler : new()
    {
        private readonly string methodName;

        public MethodInvokePipeline(IBehaviorCollection collection, IReflectionBehaviorInstanceProvider behaviorInstance, string methodName)
            : base(collection, behaviorInstance)
        {
            Guard.NotNullOrEmpty(methodName, "methodName");
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
            Guard.NotNull(parameters, "parameters");

            IEnumerable<IBehavior<THandler>> behaviors = GetBehaviors();
            IActivator<THandler> handlerFactory = GetHandlerFactory();
            THandler handler = handlerFactory.Create();

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
