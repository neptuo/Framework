using Neptuo.Activators;
using Neptuo.Linq.Expressions;
using Neptuo.Queries.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Queries
{
    /// <summary>
    /// An implementation of the <see cref="IQueryDispatcher"/> which uses a <see cref="IDependencyProvider"/> to read registrations of a <see cref="IQueryHandler{TQuery, TResult}"/>.
    /// </summary>
    public class DependencyQueryDispatcher : IQueryDispatcher
    {
        private IDependencyProvider dependencyProvider;
        
        /// <summary>
        /// Creates a new instance with a <paramref name="dependencyProvider"/>.
        /// </summary>
        /// <param name="dependencyProvider">A query handler provider.</param>
        public DependencyQueryDispatcher(IDependencyProvider dependencyProvider)
        {
            Ensure.NotNull(dependencyProvider, "dependencyProvider");
            this.dependencyProvider = dependencyProvider;
        }

        public Task<TResult> QueryAsync<TQuery, TResult>(TQuery query)
            where TQuery : IQuery<TResult>
        {
            Ensure.NotNull(query, "query");

            IQueryHandler<TQuery, TResult> handler = dependencyProvider.Resolve<IQueryHandler<TQuery, TResult>>();
            return handler.HandleAsync(query);
        }
    }
}
