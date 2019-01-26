using Neptuo.Models.Keys;
using Neptuo.Models.Deleters.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neptuo.Collections.Specialized;

namespace Neptuo.Models.Deleters
{
    /// <summary>
    /// Default implementation of <see cref="IDeleteDispatcher"/>.
    /// </summary>
    public class DefaultDeleteDispatcher : IDeleteDispatcher
    {
        private readonly object handlersLock = new object();
        private readonly object searchHandlerLock = new object();
        private Dictionary<string, IDeleteHandler> handlers = new Dictionary<string, IDeleteHandler>();
        private OutFuncCollection<string, IDeleteHandler, bool> onSearchHandler = new OutFuncCollection<string, IDeleteHandler, bool>();

        public DefaultDeleteDispatcher Add(string objectType, IDeleteHandler handler)
        {
            Ensure.NotNull(objectType, "objectType");
            Ensure.NotNull(handler, "handler");

            lock (handlersLock)
                handlers[objectType] = handler;

            return this;
        }

        /// <summary>
        /// Registers search handler when non handler was found for key.
        /// </summary>
        /// <param name="searchHandler">Search handler.</param>
        /// <returns>Self (for fluency).</returns>
        public DefaultDeleteDispatcher AddSearchHandler(OutFunc<string, IDeleteHandler, bool> searchHandler)
        {
            Ensure.NotNull(searchHandler, "searchHandler");

            lock (searchHandlerLock)
                onSearchHandler.Add(searchHandler);

            return this;
        }

        public Task<IDeleteContext> PrepareContextAsync(IKey key)
        {
            Ensure.NotNull(key, "key");

            IDeleteHandler handler;
            if (handlers.TryGetValue(key.Type, out handler))
                return handler.HandleAsync(key);

            if (onSearchHandler.TryExecute(key.Type, out handler))
                return handler.HandleAsync(key);

            return Task.FromResult<IDeleteContext>(new MissingHandlerContext(key));
        }
    }
}
