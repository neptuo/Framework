using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Configuration
{
    public class DictionaryConfigurationStorage : IConfigurationStorage
    {
        private Dictionary<string, string> values;

        public DictionaryConfigurationStorage(Dictionary<string, string> values)
        {
            Guard.NotNull(values, "values");
            this.values = values;
        }

        public bool TryGetValue(string key, out string value)
        {
            value = null;
            return true;
        }
    }
}
