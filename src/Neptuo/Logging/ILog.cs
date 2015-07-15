using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Logging
{
    /// <summary>
    /// Composite log contract.
    /// Provides facility for scoping logs, for appending writers and for logging.
    /// </summary>
    public interface ILog : ILogFactory, ILogWriter
    {
    }
}
