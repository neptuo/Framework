using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Processing.Metadata
{
    public interface IValueInputProvider
    {
        object Import(Type depedencyType);
    }
}
