using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Dependencies.Providers
{
    public interface ITaskExportDescriptor
    {
        ITaskDependencyTarget Target { get; }

        object GetValue(IBootstrapTask task);
    }
}
