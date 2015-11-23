using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Events
{
    /// <summary>
    /// Event raised when file is renamed.
    /// </summary>
    public class FileRenamed
    {
        /// <summary>
        /// Renamed file.
        /// </summary>
        public IFile File { get; private set; }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="file">Renamed file.</param>
        public FileRenamed(IFile file)
        {
            Ensure.NotNull(file, "file");
            File = file;
        }
    }
}
