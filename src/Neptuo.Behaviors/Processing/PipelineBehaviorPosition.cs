using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Behaviors.Processing
{
    /// <summary>
    /// Defines positions where behaviors can be injected.
    /// </summary>
    public enum PipelineBehaviorPosition
    {
        /// <summary>
        /// Inject behavior before all dynamically obtained behaviors.
        /// </summary>
        Before,

        /// <summary>
        /// Inject behavior into dynamically obtained behavior list.
        /// </summary>
        Inside,

        /// <summary>
        /// Inject behavior after all dynamically obtainer behaviors.
        /// </summary>
        After
    }
}
