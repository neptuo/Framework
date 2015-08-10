using Neptuo.Services.Operations.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Operations
{
    /// <summary>
    /// Default implementation of <see cref="IOperationDispatcher"/> and <see cref="IOperationHandlerCollection"/>.
    /// When handling operation and operation handler is missing, exception is thrown.
    /// </summary>
    public class DefaultOperationDispatcher : IOperationHandlerCollection, IOperationDispatcher
    {
        private readonly Dictionary<Type, Dictionary<Type, object>> storage = new Dictionary<Type, Dictionary<Type, object>>();

        public IOperationHandlerCollection Add<TInput, TOutput>(IOperationHandler<TInput, TOutput> handler)
        {
            Ensure.NotNull(handler, "handler");
            Type inputType = typeof(TInput);
            Type outputType = typeof(TOutput);

            Dictionary<Type, object> inputStorage;
            if (!storage.TryGetValue(inputType, out inputStorage))
                storage[inputType] = inputStorage = new Dictionary<Type, object>();

            inputStorage[outputType] = handler;
            return this;
        }

        public bool TryGet<TInput, TOutput>(out IOperationHandler<TInput, TOutput> handler)
        {
            Type inputType = typeof(TInput);
            Type outputType = typeof(TOutput);

            object innerHandler;
            Dictionary<Type, object> inputStorage;
            if (storage.TryGetValue(inputType, out inputStorage) && inputStorage.TryGetValue(outputType, out innerHandler))
            {
                handler = (IOperationHandler<TInput, TOutput>)innerHandler;
                return true;
            }

            handler = null;
            return false;
        }

        public Task<TOutput> ExecuteAsync<TInput, TOutput>(TInput request)
        {
            IOperationHandler<TInput, TOutput> handler;
            if (TryGet(out handler))
                return handler.HandleAsync(request);

            throw Ensure.Exception.ArgumentOutOfRange(
                "request", 
                "There isn't operation handler for input of type '{0}' and output of type '{1}'.", 
                typeof(TInput).FullName, 
                typeof(TOutput).FullName
            );
        }
    }
}
