using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Processing.Metadata
{
    public interface IValueOutputProvider
    {
        void Export(Type dependencyType, object instance);
    }
}
