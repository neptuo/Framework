using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ObjectBuilder
{
    public interface IDependencyLifetime
    {
        object GetValue();
        void RemoveValue();
        void SetValue(object newValue);
    }
}
