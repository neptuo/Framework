using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators.AutoExports
{
    /// <summary>
    /// Exports service as transient.
    /// </summary>
    public class ExportTransientAttribute : Attribute
    {
        {
            return DependencyLifetime.Transient;
        }
    }
}
