using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Exceptions.Handlers
{
    /// <summary>
    /// The factory class for exception handlers from actions.
    /// </summary>
    public static class DelegateExceptionHandler
    {
        /// <summary>
        /// Creates new instance using <paramref name="handler"/>.
        /// </summary>
        /// <param name="handler">Delegate for handling exceptions.</param>
        public static IExceptionHandler FromAction(Action<Exception> handler)
        {
            Ensure.NotNull(handler, "action");
            return new ExceptionHandler(handler);
        }

        private class ExceptionHandler : IExceptionHandler
        {
            private readonly Action<Exception> handler;

            public ExceptionHandler(Action<Exception> handler)
            {
                this.handler = handler;
            }

            public void Handle(Exception exception)
            {
                handler(exception);
            }
        }
    }
}
