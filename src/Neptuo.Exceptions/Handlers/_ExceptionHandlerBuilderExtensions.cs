using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Exceptions.Handlers
{
    /// <summary>
    /// A common extensions for <see cref="ExceptionHandlerBuilder{T}"/>.
    /// </summary>
    public static class _ExceptionHandlerBuilderExtensions
    {
        /// <summary>
        /// Registers handler which marks all exceptions as handled.
        /// </summary>
        /// <returns>Self (for fluency).</returns>
        public static ExceptionHandlerBuilder<T> Ignore<T>(this ExceptionHandlerBuilder<T> builder)
            where T : Exception
        {
            Ensure.NotNull(builder, "builder");
            return builder.Handler(new IgnoreExceptionHandler<T>());
        }
    }
}
