using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Timers.Handlers
{
    /// <summary>
    /// Base contract for invokable service running in background.
    /// </summary>
    public interface IBackgroundHandler
    {
        /// <summary>
        /// Should to it's stuff.
        /// </summary>
        Task InvokeAsync();
    }
}
