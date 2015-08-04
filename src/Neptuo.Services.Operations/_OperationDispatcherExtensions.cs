using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Operations
{
    /// <summary>
    /// Common extensions for <see cref="IOperationDispatcher"/>.
    /// </summary>
    public static class _OperationDispatcherExtensions
    {
        /// <summary>
        /// Executes <paramref name="request"/> and returns its response.
        /// </summary>
        /// <typeparam name="TInput">Type of request.</typeparam>
        /// <typeparam name="TOutput">Type of response.</typeparam>
        /// <param name="request">Request data.</param>
        /// <returns>Response to <paramref name="request"/>.</returns>
        public static Task<TOutput> Execute<TInput, TOutput>(this IOperationDispatcher mediator, TInput request)
            where TInput : IOperation<TOutput>
        {
            Ensure.NotNull(mediator, "mediator");
            return mediator.ExecuteAsync<TInput, TOutput>(request);
        }
    }
}
