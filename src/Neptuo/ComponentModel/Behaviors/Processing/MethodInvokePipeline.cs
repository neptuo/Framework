using Neptuo.ComponentModel.Behaviors.Processing.Reflection;
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

        public async Task<TOutput> ExecuteAsync(params object[] parameters)
        {
            Guard.NotNull(parameters, "parameters");

            IEnumerable<IBehavior<THandler>> behaviors = GetBehaviors();
            IActivator<THandler> handlerFactory = GetHandlerFactory();
            THandler handler = handlerFactory.Create();

            IBehaviorContext context = new MethodInfoBehaviorContext<THandler>(behaviors, handler, GetMethodInfo())
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
    }
}
