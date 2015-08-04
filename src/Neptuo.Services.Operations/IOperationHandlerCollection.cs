using Neptuo.Services.Operations.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Operations
{
    /// <summary>
    /// Collection of registered operation handlers.
    /// </summary>
    public interface IOperationHandlerCollection
    {
        /// <summary>
        /// Registers <paramref name="handler"/> to handle operations for input of type <typeparamref name="TInput"/> and output of type <typeparamref name="TOutput"/>
        /// </summary>
        /// <typeparam name="TInput">Type of input.</typeparam>
        /// <typeparam name="TOutput">Type of output.</typeparam>
        /// <param name="handler">Handler for input of type <typeparamref name="TInput"/> and output of type <typeparamref name="TOutput"/>.</param>
        /// <returns>Self (for fluency).</returns>
        IOperationHandlerCollection Add<TInput, TOutput>(IOperationHandler<TInput, TOutput> handler);

        /// <summary>
        /// Tries to find operation handler for input of type <typeparamref name="TInput"/> and output of type <typeparamref name="TOutput"/>.
        /// </summary>
        /// <typeparam name="TInput">Type of input.</typeparam>
        /// <typeparam name="TOutput">Type of output.</typeparam>
        /// <param name="handler">Handler for input of type <typeparamref name="TInput"/> and output of type <typeparamref name="TOutput"/>.</param>
        /// <returns><c>true</c> if such a handler is registered; <c>false</c> otherwise.</returns>
        bool TryGet<TInput, TOutput>(out IOperationHandler<TInput, TOutput> handler);
    }
}
