using Neptuo.Activators;
using Neptuo.Pipelines.Commands.Interception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Pipelines.Commands.Execution
{
    public class DependencyCommandExecutorFactory : ICommandExecutorFactory
    {
        /// <summary>
        /// Current dependency provider.
        /// </summary>
        private IDependencyProvider dependencyProvider;

        /// <summary>
        /// Current provider for interceptors.
        /// </summary>
        private IInterceptorProvider interceptorProvider;

        /// <summary>
        /// Initializes new instance with <paramref name="dependencyProvider"/> and without interception.
        /// </summary>
        public DependencyCommandExecutorFactory(IDependencyProvider dependencyProvider)
            : this(dependencyProvider, new ManualInterceptorProvider(dependencyProvider))
        { }

        /// <summary>
        /// Initializes new instance with <paramref name="dependencyProvider"/>.
        /// </summary>
        /// <param name="dependencyProvider">Source for registrations.</param>
        /// <param name="interceptorProvider">Interceptor provider.</param>
        public DependencyCommandExecutorFactory(IDependencyProvider dependencyProvider, IInterceptorProvider interceptorProvider)
        {
            Guard.NotNull(dependencyProvider, "dependencyProvider");
            Guard.NotNull(interceptorProvider, "interceptorProvider");
            this.dependencyProvider = dependencyProvider;
            this.interceptorProvider = interceptorProvider;
        }

        /// <summary>
        /// Creates <see cref="DependencyCommandExecutor"/>.
        /// </summary>
        /// <param name="command">Command instance.</param>
        /// <returns>Instance of <see cref="DependencyCommandExecutor"/>.</returns>
        public ICommandExecutor CreateExecutor(object command)
        {
            return new DependencyCommandExecutor(dependencyProvider, interceptorProvider);
        }
    }
}
