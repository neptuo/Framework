using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Events
{
    /// <summary>
    /// Event raised when directory is renamed.
    /// </summary>
    public class DirectoryRenamed
    {
        /// <summary>
        /// Renamed directory.
        /// </summary>
        public IDirectory Directory { get; private set; }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="directory">Renamed directory.</param>
        public DirectoryRenamed(IDirectory directory)
        {
            Ensure.NotNull(directory, "directory");
            Directory = directory;
        }
    }
}
