using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel.Behaviors
{
    /// <summary>
    /// Integrates logic into execution pipeline.
    /// </summary>
    /// <typeparam name="T">Type of required behavior interface.</typeparam>
    public interface IBehavior<in T>
    {
        /// <summary>
        /// Invoked when processing pipeline.
        /// </summary>
        /// <param name="handler">Behavior interface.</param>
        /// <param name="context">Current behavior context.</param>
        Task ExecuteAsync(T handler, IBehaviorContext context);
    }
}
