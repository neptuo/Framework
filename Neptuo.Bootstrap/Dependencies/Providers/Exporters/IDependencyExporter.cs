using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Dependencies.Providers.Exporters
{
    public interface IDependencyExporter
    {
        void Export(IDependencyExportDescriptor export, object value);
    }
}
