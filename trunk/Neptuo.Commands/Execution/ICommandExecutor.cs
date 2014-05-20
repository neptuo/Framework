using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands.Execution
{
    /// <summary>
    /// Executor for command.
    /// </summary>
    public interface ICommandExecutor
    {
        /// <summary>
        /// Executes command.
        /// </summary>
        /// <param name="command">Command.</param>
        void Handle(object command);
    }
}
