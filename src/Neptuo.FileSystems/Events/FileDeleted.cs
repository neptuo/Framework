using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Events
{
    /// <summary>
    /// Event raised when file is deleted.
    /// </summary>
    public class FileDeleted
    {
        /// <summary>
        /// Deleted file.
        /// </summary>
        public IFile File { get; private set; }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="file">Deleted file.</param>
        public FileDeleted(IFile file)
        {
            Ensure.NotNull(file, "file");
            File = file;
        }
    }
}
