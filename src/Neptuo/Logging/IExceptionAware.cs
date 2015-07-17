using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Logging
{
    /// <summary>
    /// Defines model, that has instance of exception.
    /// </summary>
    public interface IExceptionAware
    {
        /// <summary>
        /// Exception instance.
        /// </summary>
        Exception Exception { get; }
    }
}
