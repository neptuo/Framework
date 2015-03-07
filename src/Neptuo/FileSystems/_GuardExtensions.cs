using Neptuo.Exceptions.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems
{
    /// <summary>
    /// Exceptions extensions on file systems.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class _GuardExtensions
    {
        /// <summary>
        /// Creates exception <see cref="FileSystemException"/> 
        /// and message formatted from <paramref name="format"/> and <paramref name="formatParameters"/>.
        /// </summary>
        /// <param name="guard"></param>
        /// <param name="format"></param>
        /// <param name="formatParameters"></param>
        /// <returns><see cref="FileSystemException"/>.</returns>
        [DebuggerStepThrough]
        public static FileSystemException FileSystem(this GuardExceptionHelper guard, string format, params object[] formatParameters)
        {
            Guard.NotNull(guard, "guard");
            Guard.NotNullOrEmpty(format, "format");
            return new FileSystemException(String.Format(format, formatParameters));
        }
    }
}
