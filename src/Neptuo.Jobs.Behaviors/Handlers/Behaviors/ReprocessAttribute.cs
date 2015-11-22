using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Jobs.Handlers.Behaviors
{
    /// <summary>
    /// Behavior attribute for action reprocessing.
    /// </summary>
    public class ReprocessAttribute : Attribute
    {
        /// <summary>
        /// Default value of max reprocess count.
        /// </summary>
        public const int DefaultReprocessCount = 3;

        /// <summary>
        /// Default of delay before reprocess.
        /// </summary>
        public const double DefaultDelayBeforeReprocess = 0;

        /// <summary>
        /// Reprocess max count.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Time to wait before starting reprocess.
        /// </summary>
        public TimeSpan DelayBeforeReprocess { get; private set; }

        /// <summary>
        /// Creates new intance with reprocess count <see cref="ReprocessAttribute.DefaultReprocessCount"/> and 
        /// <see cref="ReprocessAttribute.DefaultDelayBeforeReprocess"/> as delay before reprocess.
        /// </summary>
        public ReprocessAttribute()
            : this(DefaultReprocessCount)
        { }

        /// <summary>
        /// Creates new instance with 0ms as delay before reprocess.
        /// </summary>
        /// <param name="count">Reprocess max count.</param>
        public ReprocessAttribute(int count)
            : this(count, DefaultDelayBeforeReprocess)
        { }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="count">Reprocess max count.</param>
        /// <param name="delayBeforeReprocess">Time (milliseconds) to wait before starting reprocess.</param>
        public ReprocessAttribute(int count, double delayBeforeReprocess)
        {
            Ensure.PositiveOrZero(count, "count");
            Ensure.PositiveOrZero(delayBeforeReprocess, "delayBeforeReprocess");
            Count = count;
            DelayBeforeReprocess = TimeSpan.FromMilliseconds(delayBeforeReprocess);
        }
    }
}
