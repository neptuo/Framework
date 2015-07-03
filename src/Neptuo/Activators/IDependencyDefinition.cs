using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators
{
    /// <summary>
    /// Describes single dependency container registration.
    /// </summary>
    public interface IDependencyDefinition
    {
        /// <summary>
        /// Type of service to be resolved by.
        /// </summary>
        Type ServiceType { get; }

        /// <summary>
        /// Lifetime of instances.
        /// </summary>
        DependencyLifetime Lifetime { get; }

        /// <summary>
        /// Creates 
        /// </summary>
        /// <returns></returns>
        object CreateInstance();
    }
}
