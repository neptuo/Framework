using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Operations.Handlers
{
    /// <summary>
    /// Request-reply handler.
    /// </summary>
    /// <typeparam name="TInput">Type of request.</typeparam>
    /// <typeparam name="TOutput">Type of response.</typeparam>
    public interface IOperationHandler<in TInput, TOutput>
    {
        /// <summary>
        /// Should process <paramref name="request" /> and return response of type <typeparamref name="TOutput"/>.
        /// </summary>
        /// <param name="request">Request parameters.</param>
        /// <returns>Response to <paramref name="request"/>.</returns>
        Task<TOutput> HandleAsync(TInput request);
    }
}
