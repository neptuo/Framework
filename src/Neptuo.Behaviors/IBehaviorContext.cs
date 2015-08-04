using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Behaviors
{
    /// <summary>
    /// Provides access to currently executing pipeline.
    /// </summary>
    public interface IBehaviorContext : ICloneable<IBehaviorContext>
    {
        /// <summary>
        /// Collection of custom context values.
        /// </summary>
        IKeyValueCollection CustomValues { get; }

        /// <summary>
        /// Promotes execution to next behavior in pipeline.
        /// </summary>
        Task NextAsync();
    }
}
