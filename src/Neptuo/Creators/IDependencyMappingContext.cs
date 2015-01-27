using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Creators
{
    /// <summary>
    /// Context pro mapping dependency.
    /// </summary>
    public interface IDependencyMappingContext
    {
        /// <summary>
        /// Calling dependency provider.
        /// </summary>
        IDependencyProvider CurrentProvider { get; }
    }
}
