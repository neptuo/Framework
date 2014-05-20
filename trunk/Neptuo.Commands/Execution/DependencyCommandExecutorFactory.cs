using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands.Execution
{
    public class DependencyCommandExecutorFactory : ICommandExecutorFactory
    {
        /// <summary>
        /// Current dependency provider.
        /// </summary>
        private IDependencyProvider dependencyProvider;

        /// <summary>
        /// Initializes new instance with <paramref name="dependencyProvider"/>.
        /// </summary>
        /// <param name="dependencyProvider">Source for registrations.</param>
        public DependencyCommandExecutorFactory(IDependencyProvider dependencyProvider)
        {
            Guard.NotNull(dependencyProvider, "dependencyProvider");
            this.dependencyProvider = dependencyProvider;
        }

        /// <summary>
        /// Creates <see cref="DependencyCommandExecutor"/>.
        /// </summary>
        /// <param name="command">Command instance.</param>
        /// <returns>Instance of <see cref="DependencyCommandExecutor"/>.</returns>
        public ICommandExecutor CreateExecutor(object command)
        {
            return new DependencyCommandExecutor(dependencyProvider);
        }
    }
}
