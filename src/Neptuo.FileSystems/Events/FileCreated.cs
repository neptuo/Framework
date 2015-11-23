using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Events
{
    /// <summary>
    /// Event raised when new file is created.
    /// </summary>
    public class FileCreated
    {
        /// <summary>
        /// New file.
        /// </summary>
        public IFile File { get; private set; }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="file">New file.</param>
        public FileCreated(IFile file)
        {
            Ensure.NotNull(file, "file");
            File = file;
        }
    }
}
