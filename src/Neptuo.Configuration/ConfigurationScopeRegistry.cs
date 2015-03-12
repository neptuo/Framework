using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Configuration
{
    public class ConfigurationScopeRegistry : IConfigurationScopeRegistry
    {
        private Dictionary<string, IConfigurationScope> storage;

        public ConfigurationScopeRegistry()
        {
            storage = new Dictionary<string, IConfigurationScope>();
        }

        public void MapScope(string name, IConfigurationScope scope)
        {
            Ensure.NotNull(name, "name");
            Ensure.NotNull(scope, "scope");
            storage[name] = scope;
        }

        public bool TryGet(string name, out IConfigurationScope scope)
        {
            return storage.TryGetValue(name, out scope);
        }
    }
}
