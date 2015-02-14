using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Pipelines
{
    public static class _MediatorExtensions
    {
        /// <summary>
        /// Executes <paramref name="request"/> and returns its response.
        /// </summary>
        /// <typeparam name="TInput">Type of request.</typeparam>
        /// <typeparam name="TOutput">Type of response.</typeparam>
        /// <param name="request">Request data.</param>
        /// <returns>Response to <paramref name="request"/>.</returns>
        public static TOutput Execute<TInput, TOutput>(this IMediator mediator, TInput request)
            where TInput : IWitResponse<TOutput>
        {
            Guard.NotNull(mediator, "mediator");
            return mediator.Execute<TInput, TOutput>(request);
        }
    }
}
