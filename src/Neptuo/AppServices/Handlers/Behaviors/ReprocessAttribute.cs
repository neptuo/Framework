﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.AppServices.Handlers.Behaviors
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
        /// Reprocess max count.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Time to wait before starting reprocess.
        /// </summary>
        public TimeSpan DelayBeforeReprocess { get; private set; }

        /// <summary>
        /// Creates new intance with reprocess count <see cref="ReprocessAttribute.DefaultReprocessCount"/> and 
        /// 0ms as delay before reprocess.
        /// </summary>
        public ReprocessAttribute()
            : this(3)
        { }

        /// <summary>
        /// Creates new instance with 0ms as delay before reprocess.
        /// </summary>
        /// <param name="count">Reprocess max count.</param>
        public ReprocessAttribute(int count)
            : this(count, 0)
        { }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="count">Reprocess max count.</param>
        /// <param name="delayBeforeReprocess">Time (milliseconds) to wait before starting reprocess.</param>
        public ReprocessAttribute(int count, double delayBeforeReprocess)
        {
            Ensure.PositiveOrZero(count, "count");
            Count = count;
            DelayBeforeReprocess = TimeSpan.FromMilliseconds(delayBeforeReprocess);
        }
    }
}