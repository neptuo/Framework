using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Configuration
{
    public class EmptyConfigurationStorage : IConfigurationStorage
    {
        public bool TryGetValue(string key, out string value)
        {
            value = null;
            return true;
        }
    }
}
