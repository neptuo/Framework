using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Commands.Interception
{
    /// <summary>
    /// Enables interception of command execution.
    /// </summary>
    public interface IDecoratedInvoke
    {
        /// <summary>
        /// Arround handler invoke.
        /// </summary>
        /// <param name="context">Context.</param>
        void OnInvoke(IDecoratedInvokeContext context);
    }
}
