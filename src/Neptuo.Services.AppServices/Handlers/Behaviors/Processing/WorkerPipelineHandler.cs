using Neptuo.Activators;
using Neptuo.AppServices.Handlers;
using Neptuo.Behaviors;
using Neptuo.Behaviors.Processing;
using Neptuo.Behaviors.Processing.Compilation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.AppServices.Handlers.Behaviors.Processing
{
    /// <summary>
    /// Base class for behaviors processing integration.
    /// </summary>
    /// <typeparam name="T">Type of inner handler.</typeparam>
    //public abstract class WorkerPipelineHandler<T> : DefaultPipelineBase<T>, IBackgroundHandler
    //    where T : IBackgroundHandler, new()
    //{
    //    protected override IBehaviorContext GetBehaviorContext(IEnumerable<IBehavior<T>> behaviors, T handler)
    //    {
    //        return new DefaultBehaviorContext<T>(
    //            Enumerable.Concat(
    //                behaviors,
    //                new IBehavior<T>[] { new InvokeBehavior() }
    //            ), 
    //            handler
    //        );
    //    }

    //    public void Invoke()
    //    {
    //        Task result = ExecutePipelineAsync();
    //        if (!result.IsCompleted)
    //            result.RunSynchronously();
    //    }

    //    private class InvokeBehavior : IBehavior<T>
    //    {
    //        public Task ExecuteAsync(T handler, IBehaviorContext context)
    //        {
    //            handler.Invoke();
    //            return Task.FromResult(true);
    //        }
    //    }
    //}
}
