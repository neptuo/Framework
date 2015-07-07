using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Commands.Execution
{
    /// <summary>
    /// Factory for <see cref="ICommandExecutor"/>.
    /// </summary>
    public interface ICommandExecutorFactory
    {
        /// <summary>
        /// Creates executor for <paramref name="command"/>.
        /// </summary>
        /// <param name="command">Command.</param>
        /// <returns>Executor for <paramref name="command"/>.</returns>
        ICommandExecutor CreateExecutor(object command);
    }
}
