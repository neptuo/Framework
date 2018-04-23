using Neptuo.Exceptions.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo
{
    /// <summary>
    /// Extensions for system exceptions.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class _EnsureSystemExtensions
    {
        /// <summary>
        /// Creates exception <see cref="NotImplementedException"/> 
        /// and optional message formatted from <paramref name="format"/> and <paramref name="formatParameters"/>.
        /// </summary>
        /// <param name="guard"></param>
        /// <param name="format"></param>
        /// <param name="formatParameters"></param>
        /// <returns><see cref="NotImplementedException"/>.</returns>
        [DebuggerStepThrough]
        public static NotImplementedException NotImplemented(this EnsureExceptionHelper guard, string format = null, params object[] formatParameters)
        {
            Ensure.NotNull(guard, "guard");

            if (String.IsNullOrEmpty(format))
                return new NotImplementedException();

            return new NotImplementedException(String.Format(format, formatParameters));
        }

        /// <summary>
        /// Creates exception <see cref="NotSupportedException"/>
        /// and optional message formatted from <paramref name="format"/> and <paramref name="formatParameters"/>.
        /// </summary>
        /// <param name="guard"></param>
        /// <param name="format"></param>
        /// <param name="formatParameters"></param>
        /// <returns><see cref="NotSupportedException"/>.</returns>
        [DebuggerStepThrough]
        public static NotSupportedException NotSupported(this EnsureExceptionHelper guard, string format = null, params object[] formatParameters)
        {
            Ensure.NotNull(guard, "guard");

            if (String.IsNullOrEmpty(format))
                return new NotSupportedException();

            return new NotSupportedException(String.Format(format, formatParameters));
        }

        /// <summary>
        /// Creates a new instance of the <see cref="NotSupportedException"/> for unsupported <paramref name="value"/> of enum <typeparamref name="TEnum"/>.
        /// </summary>
        /// <typeparam name="TEnum">A type of the enum.</typeparam>
        /// <param name="ensure">An exception helper.</param>
        /// <param name="value">The not supported value from <typeparamref name="TEnum"/>.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static NotSupportedException NotSupported<TEnum>(this EnsureExceptionHelper ensure, TEnum value)
            where TEnum : struct
        {
            return NotSupported(ensure, "The value '{0}' from the '{1}' is not supported in this context.", value, typeof(TEnum).FullName);
        }

        /// <summary>
        /// Creates exception <see cref="InvalidOperationException"/>
        /// and message formatted from <paramref name="format"/> and <paramref name="formatParameters"/>.
        /// </summary>
        /// <param name="guard"></param>
        /// <param name="format"></param>
        /// <param name="formatParameters"></param>
        /// <returns><see cref="InvalidOperationException"/>.</returns>
        [DebuggerStepThrough]
        public static InvalidOperationException InvalidOperation(this EnsureExceptionHelper guard, string format, params object[] formatParameters)
        {
            Ensure.NotNull(guard, "guard");
            Ensure.NotNullOrEmpty(format, "format");
            return new InvalidOperationException(String.Format(format, formatParameters));
        }
    }
}
