using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Events
{
    /// <summary>
    /// Event raised when directory is deleted.
    /// </summary>
    public class DirectoryDeleted
    {
        /// <summary>
        /// Deleted directory.
        /// </summary>
        public IDirectory Directory { get; private set; }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="directory">Deleted directory.</param>
        public DirectoryDeleted(IDirectory directory)
        {
            Ensure.NotNull(directory, "directory");
            Directory = directory;
        }
    }
}
