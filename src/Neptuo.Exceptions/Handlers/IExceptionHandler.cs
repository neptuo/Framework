using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Exceptions.Handlers
{
    /// <summary>
    /// The handler for exception raised during exection.
    /// </summary>
    public interface IExceptionHandler
    {
        /// <summary>
        /// Processes <paramref name="exception"/> raised during execution.
        /// </summary>
        /// <param name="exception">The exception to process.</param>
        void Handle(Exception exception);
    }
}
