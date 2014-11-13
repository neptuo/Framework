using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Dependencies.Providers
{
    public interface ITaskDependencyProvider
    {
        IEnumerable<ITaskImportDescriptor> GetImports(Type taskType);
        IEnumerable<ITaskExportDescriptor> GetExports(Type taskType);
    }
}
