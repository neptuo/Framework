using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Configuration
{
    public class StringConfigurationProperty : ConfigurationProperty<string>
    {
        public StringConfigurationProperty(string defaultValue, string scopeName, string name = null)
            : base(defaultValue, scopeName, name)
        { }

        protected override bool TryParseValue(string source, out string target)
        {
            target = source;
            return true;
        }
    }
}
