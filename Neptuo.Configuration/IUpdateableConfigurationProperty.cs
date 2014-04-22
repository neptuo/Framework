using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Configuration
{
    public interface IUpdateableConfigurationProperty
    {
        void SetValue(string value);
    }
}
