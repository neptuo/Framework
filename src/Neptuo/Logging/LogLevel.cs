using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Logging
{
    /// <summary>
    /// Enumeration of level message levels.
    /// </summary>
    public enum LogLevel
    {
        Debug = 1,
        Info = 2,
        Warning = 4,
        Error = 8,
        Fatal = 16
    }
}
