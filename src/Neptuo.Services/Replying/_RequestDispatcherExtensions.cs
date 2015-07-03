using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Replying
{
    /// <summary>
    /// Common extensions for <see cref="IRequestDispatcher"/>.
    /// </summary>
    public static class _RequestDispatcherExtensions
    {
        /// <summary>
        /// Executes <paramref name="request"/> and returns its response.
        /// </summary>
        /// <typeparam name="TInput">Type of request.</typeparam>
        /// <typeparam name="TOutput">Type of response.</typeparam>
        /// <param name="request">Request data.</param>
        /// <returns>Response to <paramref name="request"/>.</returns>
        public static Task<TOutput> Execute<TInput, TOutput>(this IRequestDispatcher mediator, TInput request)
            where TInput : IRequest<TOutput>
        {
            Ensure.NotNull(mediator, "mediator");
            return mediator.ExecuteAsync<TInput, TOutput>(request);
        }
    }
}
