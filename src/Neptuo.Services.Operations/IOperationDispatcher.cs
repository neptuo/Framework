using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Operations
{
    /// <summary>
    /// Request-reply pipeline.
    /// </summary>
    public interface IOperationDispatcher
    {
        /// <summary>
        /// Executes <paramref name="request"/> and returns its response.
        /// </summary>
        /// <typeparam name="TInput">Type of request.</typeparam>
        /// <typeparam name="TOutput">Type of response.</typeparam>
        /// <param name="request">Request data.</param>
        /// <returns>Response to <paramref name="request"/>.</returns>
        Task<TOutput> ExecuteAsync<TInput, TOutput>(TInput request);
    }
}
