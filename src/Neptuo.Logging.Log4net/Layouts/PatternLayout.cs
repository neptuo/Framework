using Neptuo.Logging.Layouts.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Logging.Layouts
{
    /// <summary>
    /// Extensiono of <see cref="log4net.Layout.PatternLayout"/> to support these patterns:
    /// - <c>stack</c>.
    /// </summary>
    public class PatternLayout : log4net.Layout.PatternLayout
    {
        public PatternLayout()
        {
            this.AddConverter("stack", typeof(StackTraceConverter));
        }
    }
}
