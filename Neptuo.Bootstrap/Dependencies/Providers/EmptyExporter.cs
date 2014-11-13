using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Dependencies.Providers
{
    public class EmptyExporter : ITaskDependencyExporter
    {
        public void Export(ITaskExportDescriptor export, object value)
        { }
    }
}
