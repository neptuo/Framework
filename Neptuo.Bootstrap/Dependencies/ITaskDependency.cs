using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Dependencies
{
    public interface ITaskDependency
    {
        Type TargetType { get; }
    }
}
