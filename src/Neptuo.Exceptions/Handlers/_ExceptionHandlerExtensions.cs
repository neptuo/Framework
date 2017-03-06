using Neptuo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Exceptions.Handlers
{
    /// <summary>
    /// A common extensions for the <see cref="IExceptionHandler"/> and <see cref="IExceptionHandler{T}"/>.
    /// </summary>
    public static class _ExceptionHandlerExtensions
    {
        /// <summary>
        /// Tries to handle <paramref name="exception"/> using <paramref name="handler"/>.
        /// If the <paramref name="handler"/> marks the context as handled, returns <c>true</c>; otherwise returns <c>false</c>.
        /// </summary>
        /// <typeparam name="T">A type of the exception.</typeparam>
        /// <param name="handler">A handler to use.</param>
        /// <param name="exception">An exception to handle.</param>
        /// <returns>If the <paramref name="handler"/> marks the context as handled, returns <c>true</c>; otherwise returns <c>false</c>.</returns>
        public static bool TryHandle<T>(this IExceptionHandler<IExceptionHandlerContext<T>> handler, T exception)
            where T : Exception
        {
            Ensure.NotNull(handler, "handler");
            IExceptionHandlerContext<T> context = new DefaultExceptionHandlerContext<T>(exception);
            handler.Handle(context);
            return context.IsHandled;
        }
    }
}
