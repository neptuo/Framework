using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Creators.Building
{
    public interface IDependencyScopeMetadata
    {
        public bool IsRegistrationSingleThread { get; }
        public bool IsResolvingSingleThread { get; }
    }
}
