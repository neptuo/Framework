using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neptuo.AppServices
{
    /// <summary>
    /// Describes collection of application services.
    /// </summary>
    public interface IServiceCollection
    {
        /// <summary>
        /// Adds service described by <paramref name="serviceDescriptor"/>.
        /// </summary>
        /// <param name="serviceDescriptor">Descriptor of service to add.</param>
        /// <returns>Self (for fluency).</returns>
        IServiceCollection Add(IServiceDescriptor serviceDescriptor);
    }
}
