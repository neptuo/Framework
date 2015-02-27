using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators.Building
{
    /// <summary>
    /// Context pro mapping dependency.
    /// </summary>
    public interface IDependencyActivatorContext
    {
        /// <summary>
        /// Calling dependency provider.
        /// </summary>
        IDependencyProvider CurrentProvider { get; }

        /// <summary>
        /// Required service.
        /// </summary>
        Type ServiceType { get; }
    }
}
