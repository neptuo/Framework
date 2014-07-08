using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Behaviors
{
    /// <summary>
    /// Provides access to currently executing pipeline.
    /// </summary>
    public interface IBehaviorContext
    {
        /// <summary>
        /// Promotes execution to next behavior in pipeline.
        /// </summary>
        void Next();
    }
}
