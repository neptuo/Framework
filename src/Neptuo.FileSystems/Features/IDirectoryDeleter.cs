using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Features
{
    /// <summary>
    /// Contract for deleting current directory.
    /// </summary>
    public interface IDirectoryDeleter
    {
        /// <summary>
        /// Deletes current directory (and all sub directories and files).
        /// </summary>
        void Delete();
    }
}
