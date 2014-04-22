using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Configuration
{
    public interface IConfigurationScopeRegistry
    {
        void MapScope(string name, IConfigurationScope scope);
        bool TryGet(string name, out IConfigurationScope scope);
    }
}
