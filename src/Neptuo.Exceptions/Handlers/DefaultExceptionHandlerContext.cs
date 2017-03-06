using Neptuo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Exceptions.Handlers
{
    /// <summary>
    /// A default implementation of the <see cref="IExceptionHandlerContext"/>.
    /// </summary>
    public class DefaultExceptionHandlerContext : IExceptionHandlerContext
    {
        public bool IsHandled { get; set; }
        public Exception Exception { get; private set; }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="exception">An exception to handle.</param>
        public DefaultExceptionHandlerContext(Exception exception)
        {
            Ensure.NotNull(exception, "exception");
            Exception = exception;
        }
    }

    /// <summary>
    /// A default implementation of the <see cref="IExceptionHandlerContext"/>.
    /// </summary>
    public class DefaultExceptionHandlerContext<T> : IExceptionHandlerContext<T>
        where T : Exception
    {
        public bool IsHandled { get; set; }
        public T Exception { get; private set; }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="exception">An exception to handle.</param>
        public DefaultExceptionHandlerContext(T exception)
        {
            Ensure.NotNull(exception, "exception");
            Exception = exception;
        }
    }
}
