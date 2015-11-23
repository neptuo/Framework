using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Events
{
    /// <summary>
    /// Event raised when new directory is created.
    /// </summary>
    public class DirectoryCreated
    {
        /// <summary>
        /// New directory.
        /// </summary>
        public IDirectory Directory { get; private set; }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="directory">New directory.</param>
        public DirectoryCreated(IDirectory directory)
        {
            Ensure.NotNull(directory, "directory");
            Directory = directory;
        }
    }
}
