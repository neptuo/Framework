using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Configuration
{
    public class EmptyConfigurationScope : IConfigurationScope
    {
        public IConfigurationStorage GetStorage()
        {
            return new EmptyConfigurationStorage();
        }
    }
}
