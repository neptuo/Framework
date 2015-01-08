using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Configuration
{
    public class IntConfigurationProperty : ConfigurationProperty<int>
    {
        public IntConfigurationProperty(int defaultValue, string scopeName, string name = null)
            : base(defaultValue, scopeName, name)
        { }

        protected override bool TryParseValue(string source, out int target)
        {
            return Int32.TryParse(source, out target);
        }
    }
}
