using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Exceptions.Handlers
{
    /// <summary>
    /// The factory class for exception handlers from actions and functions.
    /// </summary>
    public static class DelegateExceptionHandler
    {
        /// <summary>
        /// Creates a new instance using <paramref name="handler"/>.
        /// </summary>
        /// <param name="handler">A delegate for handling exceptions.</param>
        public static IExceptionHandler FromAction(Action<Exception> handler)
        {
            Ensure.NotNull(handler, "action");
            return new ExceptionHandler(handler);
        }

        /// <summary>
        /// Creates a new instance using <paramref name="handler"/>.
        /// </summary>
        /// <typeparam name="T">A type of an exception to handle.</typeparam>
        /// <param name="handler">A delegate for handling exceptions.</param>
        public static IExceptionHandler<T> FromAction<T>(Action<T> handler)
            where T : Exception
        {
            Ensure.NotNull(handler, "action");
            return new ExceptionHandler<T>(handler);
        }

        /// <summary>
        /// Creates a new instance using <paramref name="handler"/>.
        /// </summary>
        /// <typeparam name="T">A type of an exception to handle.</typeparam>
        /// <param name="handler">A delegate for handling exceptions.</param>
        public static IExceptionHandler<IExceptionHandlerContext<T>> FromAction<T>(Action<IExceptionHandlerContext<T>> handler)
            where T : Exception
        {
            Ensure.NotNull(handler, "action");
            return new ExceptionHandler<IExceptionHandlerContext<T>>(handler);
        }

        /// <summary>
        /// Creates a new instance using <paramref name="handler"/>.
        /// </summary>
        /// <typeparam name="T">A type of an exception to handle.</typeparam>
        /// <param name="handler">A delegate for handling exceptions.</param>
        public static IExceptionHandler<IExceptionHandlerContext<T>> FromFunc<T>(Func<T, bool> handler)
            where T : Exception
        {
            Ensure.NotNull(handler, "action");
            return new ExceptionHandler<IExceptionHandlerContext<T>>(context =>
            {
                if (handler(context.Exception))
                    context.IsHandled = true;
            });
        }

        private class ExceptionHandler : ExceptionHandler<Exception>, IExceptionHandler
        {
            public ExceptionHandler(Action<Exception> handler)
                : base(handler)
            { }
        }

        private class ExceptionHandler<T> : IExceptionHandler<T>
        {
            private readonly Action<T> handler;

            public ExceptionHandler(Action<T> handler)
            {
                this.handler = handler;
            }

            public void Handle(T exception)
            {
                handler(exception);
            }
        }
    }
}
