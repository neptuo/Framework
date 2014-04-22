using Neptuo.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Configuration
{
    class StaticConfigurationScope : IConfigurationScope
    {
        public IConfigurationStorage GetStorage()
        {
            return new ConfigurationStorage();
        }

        class ConfigurationStorage : IConfigurationStorage
        {
            public bool TryGetValue(string key, out string value)
            {
                value = "True";
                return true;
            }
        }
    }
}
