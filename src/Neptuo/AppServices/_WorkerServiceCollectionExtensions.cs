using Neptuo.AppServices.Handlers;
using Neptuo.AppServices.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.AppServices
{
    /// <summary>
    /// Common trigger extensions for <see cref="WorkerServiceCollection"/>.
    /// </summary>
    public static class _WorkerServiceCollectionExtensions
    {
        /// <summary>
        /// Adds <paramref name="handler"/> triggered every <paramref name="interval"/>.
        /// </summary>
        /// <param name="collection">Target collection of background workers.</param>
        /// <param name="interval">Amount of time between trigger hits.</param>
        /// <param name="handler">Handler to execute.</param>
        /// <returns>Self (for fluency).</returns>
        public static WorkerServiceCollection AddIntervalHandler(this WorkerServiceCollection collection, TimeSpan interval, IBackgroundHandler handler)
        {
            Guard.NotNull(collection, "collection");
            Guard.NotNull(handler, "handler");
            return collection.AddHandler(new TimerServiceTrigger(interval), handler);
        }

        /// <summary>
        /// Adds <paramref name="handler"/> triggered every <paramref name="interval"/>.
        /// </summary>
        /// <param name="collection">Target collection of background workers.</param>
        /// <param name="startDelay">Amount of time to first trigger hit after start.</param>
        /// <param name="interval">Amount of time between trigger hits.</param>
        /// <param name="handler">Handler to execute.</param>
        /// <returns>Self (for fluency).</returns>
        public static WorkerServiceCollection AddIntervalDelayedHandler(this WorkerServiceCollection collection, TimeSpan startDelay, TimeSpan interval, IBackgroundHandler handler)
        {
            Guard.NotNull(collection, "collection");
            Guard.NotNull(handler, "handler");
            return collection.AddHandler(new TimerServiceTrigger(startDelay, interval), handler);
        }
    }
}
