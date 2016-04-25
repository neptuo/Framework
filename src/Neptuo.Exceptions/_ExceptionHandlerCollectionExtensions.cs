using Neptuo.Exceptions.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Exceptions
{
    /// <summary>
    /// The common extensions for the <see cref="IExceptionHandlerCollection"/>.
    /// </summary>
    public static class _ExceptionHandlerCollectionExtensions
    {
        /// <summary>
        /// Enumerates all handlers in the <paramref name="collection"/> and passes each of them <paramref name="exception"/>.
        /// </summary>
        /// <param name="collection">The collection use handlers from.</param>
        /// <param name="exception">The exception to handle.</param>
        public static void Handle(this IExceptionHandlerCollection collection, Exception exception)
        {
            Ensure.NotNull(collection, "collection");
            Ensure.NotNull(exception, "exception");

            foreach (IExceptionHandler handler in collection.Enumerate())
                handler.Handle(exception);
        }
    }
}
