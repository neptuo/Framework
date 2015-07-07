using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems
{
    /// <summary>
    /// Represents directory in virtual file system.
    /// </summary>
    public interface IDirectory : IReadOnlyDirectory
    {

        /// <summary>
        /// Creates new sub directory in this directory.
        /// </summary>
        /// <param name="directoryName">Name of new directory, must be unique.</param>
        /// <returns>Newly create directory.</returns>
        Task<IDirectory> CreateDirectory(string directoryName);

        /// <summary>
        /// Creats new empty file with name <paramref name="fileName"/>.
        /// </summary>
        /// <param name="fileName">New file name.</param>
        /// <returns>Newly created file.</returns>
        Task<IFile> CreateFile(string fileName);
    }
}
