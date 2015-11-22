using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Jobs.Handlers
{
    /// <summary>
    /// Base contract for application service.
    /// </summary>
    public interface IServiceHandler : IDisposable
    {
        /// <summary>
        /// Gets current service state.
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// Starts service.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops service.
        /// </summary>
        void Stop();
    }
}
