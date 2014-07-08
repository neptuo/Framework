using Neptuo.Web.Services.Hosting.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting
{
    /// <summary>
    /// Creates instances of handlers using <see cref="IDependencyProvider"/>.
    /// </summary>
    /// <typeparam name="T">Handler type to create.</typeparam>
    public class DependencyHandlerFactory<T>: IHandlerFactory<T>
    {
        private IDependencyProvider dependencyProvider;

        /// <summary>
        /// Creates new instance with <paramref name="dependencyProvider"/> as instance creator.
        /// </summary>
        /// <param name="dependencyProvider">Dependency provider for resolving instances of <typeparamref name="T"/>.</param>
        public DependencyHandlerFactory(IDependencyProvider dependencyProvider)
        {
            Guard.NotNull(dependencyProvider, "dependencyProvider");
            this.dependencyProvider = dependencyProvider;
        }

        public T Create(IHttpContext context)
        {
            return dependencyProvider.Resolve<T>();
        }
    }
}
