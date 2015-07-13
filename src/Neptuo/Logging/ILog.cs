using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Logging
{
    /// <summary>
    /// Log contract.
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// Dot separated log scope.
        /// </summary>
        string ScopeName { get; }
    }
}
