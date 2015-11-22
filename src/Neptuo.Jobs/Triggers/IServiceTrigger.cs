using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Jobs.Triggers
{
    /// <summary>
    /// Service execution trigger.
    /// </summary>
    public interface IServiceTrigger
    {
        /// <summary>
        /// On trigger hit.
        /// </summary>
        event Action OnTrigger;

        /// <summary>
        /// Start trigger listening.
        /// </summary>
        void Start();

        /// <summary>
        /// Stop trigger listening.
        /// </summary>
        void Stop();
    }
}
