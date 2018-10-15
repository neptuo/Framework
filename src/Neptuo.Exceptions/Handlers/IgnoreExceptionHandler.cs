using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Exceptions.Handlers
{
    /// <summary>
    /// An exception handler which marks all exceptions as handled.
    /// </summary>
    /// <typeparam name="T">A type of the exception.</typeparam>
    public class IgnoreExceptionHandler : IExceptionHandler<IExceptionHandlerContext>
    {
        public void Handle(IExceptionHandlerContext context)
            => context.IsHandled = true;
    }

    /// <summary>
    /// An exception handler which marks all exceptions as handled.
    /// </summary>
    /// <typeparam name="T">A type of the exception.</typeparam>
    public class IgnoreExceptionHandler<T> : IExceptionHandler<IExceptionHandlerContext<T>>
        where T : Exception
    {
        public void Handle(IExceptionHandlerContext<T> context)
            => context.IsHandled = true;
    }
}
