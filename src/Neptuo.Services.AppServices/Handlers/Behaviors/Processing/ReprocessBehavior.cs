using Neptuo.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neptuo.AppServices.Handlers.Behaviors.Processing
{
    /// <summary>
    /// Behavior which restart processing after exception.
    /// </summary>
    public class ReprocessBehavior : IBehavior<object>
    {
        /// <summary>
        /// Default value of max reprocess count.
        /// </summary>
        public const int DefaultReprocessCount = 3;

        private readonly int count;
        private readonly TimeSpan deplayBeforeReprocess;

        /// <summary>
        /// Creates new intance with reprocess count <see cref="ReprocessBehavior.DefaultReprocessCount"/> and 
        /// 0ms as delay before reprocess.
        /// </summary>
        public ReprocessBehavior()
            : this(DefaultReprocessCount)
        { }

        /// <summary>
        /// Creates new instance with 0ms as delay before reprocess.
        /// </summary>
        /// <param name="count">Reprocess max count.</param>
        public ReprocessBehavior(int count)
            : this(count, TimeSpan.Zero)
        { }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="count">Reprocess max count.</param>
        /// <param name="deplayBeforeReprocess">Time to wait before starting reprocess.</param>
        public ReprocessBehavior(int count, TimeSpan deplayBeforeReprocess)
        {
            Ensure.PositiveOrZero(count, "count");
            this.count = count;
            this.deplayBeforeReprocess = deplayBeforeReprocess;
        }

        public Task ExecuteAsync(object handler, IBehaviorContext context)
        {
            return ExecuteAsync(handler, context, count);
        }

        private Task ExecuteAsync(object handler, IBehaviorContext context, int remaingCount)
        {
            IBehaviorContext contextState = context.Clone();
            try
            {
                return context.NextAsync();
            }
            catch (Exception e)
            {
                remaingCount--;
                if (remaingCount > 0)
                {
                    int delay = (int)deplayBeforeReprocess.TotalMilliseconds;
                    if(delay > 0)
                        Thread.Sleep(delay);

                    Task result = ExecuteAsync(handler, contextState, remaingCount);
                    //context.CustomValues = contextState.CustomValues;
                    return result;
                }

                throw e;
            }
        }
    }
}
