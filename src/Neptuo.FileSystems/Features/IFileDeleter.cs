using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Features
{
    /// <summary>
    /// Contract for deleting current file.
    /// </summary>
    public interface IFileDeleter
    {
        /// <summary>
        /// Deletes current file.
        /// </summary>
        void Delete();
    }
}
