using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neptuo.Queries
{
    /// <summary>
    /// A dispatcher for queries.
    /// </summary>
    public interface IQueryDispatcher
    {
        /// <summary>
        /// Dispatches the <paramref name="query"/> for providing result.
        /// </summary>
        /// <typeparam name="TQuery">A type of the query.</typeparam>
        /// <typeparam name="TResult">A type of the result.</typeparam>
        /// <param name="query">A query parameters.</param>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        /// <returns>Result to the <paramref name="query"/>.</returns>
        Task<TResult> QueryAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default) where TQuery : IQuery<TResult>;
    }
}
