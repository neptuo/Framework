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
    public interface IDependencyContext
    {
        /// <summary>
        /// Configuration of the container.
        /// </summary>
        IDependencyConfiguration Configuration { get; }

        /// <summary>
        /// Calling dependency provider.
        /// </summary>
        IDependencyProvider CurrentProvider { get; }

        /// <summary>
        /// Required service.
        /// </summary>
        Type ServiceType { get; }

        /// <summary>
        /// Creates instance of type <paramref name="type"/>.
        /// </summary>
        /// <param name="type">Type to instantiate.</param>
        /// <returns>Instance of <paramref name="type"/>.</returns>
        object CreateInstance(Type type);
    }
}
