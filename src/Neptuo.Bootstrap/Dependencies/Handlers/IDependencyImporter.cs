using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Dependencies.Handlers
{
    public interface IDependencyImporter
    {
        object Import(Type depedencyType);
    }
}
