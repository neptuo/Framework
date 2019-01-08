using Neptuo.Queries.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Queries
{
    /// <summary>
    /// Collection of registered query handlers.
    /// </summary>
    public interface IQueryHandlerCollection
    {
        /// <summary>
        /// Registers a <paramref name="handler"/> to handle queries of a type <typeparamref name="TQuery"/>.
        /// </summary>
        /// <typeparam name="TQuery">A type of the query.</typeparam>
        /// <typeparam name="TResult">A type of the result.</typeparam>
        /// <param name="handler">A handler for queries of the type <typeparamref name="TQuery"/>.</param>
        /// <returns>Self (for fluency).</returns>
        IQueryHandlerCollection Add<TQuery, TResult>(IQueryHandler<TQuery, TResult> handler) where TQuery : IQuery<TResult>;

        /// <summary>
        /// Tries to find a query handler for a query of a type <typeparamref name="TQuery"/>.
        /// </summary>
        /// <typeparam name="TQuery">A type of the query.</typeparam>
        /// <typeparam name="TResult">A type of the result.</typeparam>
        /// <param name="handler">A handler for queries of the type <typeparamref name="TQuery"/>.</param>
        /// <returns><c>true</c> if such a handler is registered; <c>false</c> otherwise.</returns>
        bool TryGet<TQuery, TResult>(out IQueryHandler<TQuery, TResult> handler) where TQuery : IQuery<TResult>;
    }
}
