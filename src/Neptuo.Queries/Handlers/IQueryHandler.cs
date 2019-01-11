using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neptuo.Queries.Handlers
{
    /// <summary>
    /// A handler for a query of a type <typeparamref name="TQuery"/>.
    /// </summary>
    /// <typeparam name="TQuery">A type of the query.</typeparam>
    /// <typeparam name="TResult">A type of the result.</typeparam>
    public interface IQueryHandler<in TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        /// <summary>
        /// Processes a <paramref name="query"/> and provides a result of the type <typeparamref name="TResult"/>.
        /// </summary>
        /// <param name="query">A query parameters.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A result to the <paramref name="query"/>.</returns>
        Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken);
    }
}
