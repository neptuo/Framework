using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Logging
{
    /// <summary>
    /// Composite log contract.
    /// </summary>
    public interface ILog : ILogWriter
    {
        /// <summary>
        /// Factory for child scopes.
        /// </summary>
        ILogFactory Factory { get; }
    }
}
