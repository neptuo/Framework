using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Configuration
{
    public interface IConfigurationStorage
    {
        bool TryGetValue(string key, out string value);
    }
}
