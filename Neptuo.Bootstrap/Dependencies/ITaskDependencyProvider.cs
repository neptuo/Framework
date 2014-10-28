using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Dependencies
{
    public interface ITaskDependencyProvider
    {
        IEnumerable<ITaskDependency> GetDependencies(Type taskType);
        IEnumerable<ITaskDependency> GetDependencies(IBootstrapTask task);
    }
}
