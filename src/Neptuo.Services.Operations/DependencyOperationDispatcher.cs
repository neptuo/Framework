using Neptuo.Activators;
using Neptuo.Services.Operations.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Operations
{
    /// <summary>
    /// Implementation of <see cref="IOperationDispatcher"/> which uses <see cref="IDependencyProvider"/> to read registrations from.
    /// </summary>
    public class DependencyOperationDispatcher : IOperationDispatcher
    {
        private IDependencyProvider dependencyProvider;
        
        /// <summary>
        /// Creates new instance with <paramref name="dependencyProvider"/>.
        /// </summary>
        /// <param name="dependencyProvider">Source of registrations.</param>
        public DependencyOperationDispatcher(IDependencyProvider dependencyProvider)
        {
            Ensure.NotNull(dependencyProvider, "dependencyProvider");
            this.dependencyProvider = dependencyProvider;
        }

        public Task<TOutput> ExecuteAsync<TInput, TOutput>(TInput request)
        {
            IOperationHandler<TInput, TOutput> handler = dependencyProvider.Resolve<IOperationHandler<TInput, TOutput>>();
            return handler.HandleAsync(request);
        }
    }
}
