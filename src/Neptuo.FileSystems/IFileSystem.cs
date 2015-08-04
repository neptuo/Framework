using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems
{
    /// <summary>
    /// Virtual file system.
    /// </summary>
    public interface IFileSystem
    {
        /// <summary>
        /// File system root directory.
        /// </summary>
        IReadOnlyDirectory RootDirectory { get; }

        /// <summary>
        /// If <c>true</c>, file system is read only.
        /// </summary>
        bool IsReadOnly { get; }

        /// <summary>
        /// Returns <c>true</c> if <paramref name="directory"/> (from this file system) is writeable.
        /// </summary>
        /// <param name="directory">Directory to test if writeable.</param>
        /// <returns><c>true</c> if <paramref name="directory"/> is wrteable; <c>false</c> otherwise.</returns>
        bool IsWriteable(IDirectory directory);

        /// <summary>
        /// Returns writeable directory for <paramref name="directory"/>.
        /// </summary>
        /// <param name="directory">Directory to turn into writeable one.</param>
        /// <returns>Writeable directory for <paramref name="directory"/>.</returns>
        /// <exception cref="FileSystemException">When <paramref name="directory"/> is read only.</exception>
        IDirectory AsWriteable(IReadOnlyDirectory directory);
    }
}
