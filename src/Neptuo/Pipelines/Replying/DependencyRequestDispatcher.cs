using Neptuo.Pipelines.Replying.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Pipelines.Replying
{
    /// <summary>
    /// Implementation of <see cref="IRequestDispatcher"/> which uses <see cref="IDependencyProvider"/> to read registrations from.
    /// </summary>
    public class DependencyRequestDispatcher : IRequestDispatcher
    {
        private IDependencyProvider dependencyProvider;
        
        /// <summary>
        /// Creates new instance with <paramref name="dependencyProvider"/>.
        /// </summary>
        /// <param name="dependencyProvider">Source for registrations.</param>
        public DependencyRequestDispatcher(IDependencyProvider dependencyProvider)
        {
            Guard.NotNull(dependencyProvider, "dependencyProvider");
            this.dependencyProvider = dependencyProvider;
        }

        public Task<TOutput> ExecuteAsync<TInput, TOutput>(TInput request)
        {
            IRequestHandler<TInput, TOutput> handler = dependencyProvider.Resolve<IRequestHandler<TInput, TOutput>>();
            return handler.HandleAsync(request);
        }
    }
}
