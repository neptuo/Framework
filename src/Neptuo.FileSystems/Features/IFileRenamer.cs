using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Features
{
    /// <summary>
    /// Contract for renaming current file.
    /// </summary>
    public interface IFileRenamer
    {
        /// <summary>
        /// Changes file name (without extension) to <paramref name="fileName" />
        /// </summary>
        /// <param name="fileName">New file name (without extension).</param>
        void ChangeName(string fileName);

        /// <summary>
        /// Changes file extension to <paramref name="fileExtension" />
        /// </summary>
        /// <param name="fileExtension">New extension.</param>
        void ChangeExtension(string fileExtension);
    }
}
