using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Features
{
    /// <summary>
    /// Defines contants for file system.
    /// </summary>
    public interface IFileSystemConstant
    {
        /// <summary>
        /// Item path separator.
        /// </summary>
        char DirectorySeparatorChar { get; }

        /// <summary>
        /// Enumeration of invalid characters for directory name.
        /// </summary>
        IEnumerable<char> InvalidDirectoryNameChars { get; }

        /// <summary>
        /// Enumeration of invalid characters for file name.
        /// </summary>
        IEnumerable<char> InvalidFileNameChars { get; }
    }
}
