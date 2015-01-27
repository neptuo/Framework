using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Creators.Building
{
    /// <summary>
    /// Instance provider for dependency container.
    /// </summary>
    public interface IDependencyMapping : IActivator<object, IDependencyMappingContext>
    { }
}
