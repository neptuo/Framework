using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Commands.Interception
{
    /// <summary>
    /// Describes interceptor execution pipeline.
    /// </summary>
    public interface IDecoratedInvokeContext
    {
        /// <summary>
        /// Command to handle.
        /// </summary>
        object Command { get; }

        /// <summary>
        /// Exception, that occured during execution.
        /// </summary>
        Exception Exception { get; set; }

        /// <summary>
        /// Process execution to next interceptor.
        /// </summary>
        void Next();
    }
}
