using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Pipelines.Commands.Execution
{
    /// <summary>
    /// Describes object that has Command and associated CommandHandler.
    /// </summary>
    public interface ICommandHandlerAware
    {
        /// <summary>
        /// Command handler to handle <see cref="Command"/>.
        /// </summary>
        object CommandHandler { get; }

        /// <summary>
        /// Command to handle.
        /// </summary>
        object Command { get; }
    }
}
