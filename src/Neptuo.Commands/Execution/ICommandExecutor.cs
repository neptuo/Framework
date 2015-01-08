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
        /// Fired when handling of command was completed.
        /// </summary>
        event Action<ICommandExecutor, object> OnCommandHandled;

        /// <summary>
        /// Executes command.
        /// </summary>
        /// <param name="command">Command.</param>
        void Handle(object command);
    }
}
