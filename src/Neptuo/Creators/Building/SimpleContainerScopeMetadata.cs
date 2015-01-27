using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Creators.Building
{
    public class SimpleContainerScopeMetadata
    {
        public bool IsRegistrationSingleThread { get; private set; }
        public bool IsResolvingSingleThread { get; private set; }

        public SimpleContainerScopeMetadata(bool isRegistrationSingleThread, bool isResolvingSingleThread)
        {

        }
    }
}
