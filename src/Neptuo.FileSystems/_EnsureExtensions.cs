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
    public static class _EnsureExtensions
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
        public static FileSystemException FileSystem(this EnsureExceptionHelper guard, string format, params object[] formatParameters)
        {
            Ensure.NotNull(guard, "guard");
            Ensure.NotNullOrEmpty(format, "format");
            return new FileSystemException(String.Format(format, formatParameters));
        }

        /// <summary>
        /// Throws <see cref="ArgumentNullException"/> when <paramref name="xmlFile"/> is <c>null</c>
        /// and <see cref="FileSystemException"/> when <paramref name="xmlFile"/> is not XML file.
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="xmlFile">File to test.</param>
        /// <param name="argumentName">File argument name.</param>
        public static void XmlFile(this EnsureConditionHelper condition, IReadOnlyFile xmlFile, string argumentName)
        {
            Ensure.NotNull(condition, "condition");
            Ensure.NotNull(xmlFile, argumentName);

            if (xmlFile.Extension.ToLowerInvariant() != ".xml")
                Ensure.Exception.FileSystem("Only xml files are supported, but got file named '{0}{1}'.", xmlFile.Name, xmlFile.Extension);
        }
    }
}
