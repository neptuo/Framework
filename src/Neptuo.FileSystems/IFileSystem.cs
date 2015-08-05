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
        IDirectory RootDirectory { get; }

        /// <summary>
        /// Item path separator.
        /// </summary>
        char PathSeparator { get; }
    }
}
