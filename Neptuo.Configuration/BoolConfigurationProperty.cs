using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Configuration
{
    public class BoolConfigurationProperty : ConfigurationProperty<bool>
    {
        public BoolConfigurationProperty(bool defaultValue, string scopeName, string name = null)
            : base(defaultValue, scopeName, name)
        { }

        protected override bool TryParseValue(string source, out bool target)
        {
            return Boolean.TryParse(source, out target);
        }
    }
}
