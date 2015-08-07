using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Features
{
    /// <summary>
    /// Contract for renaming directory.
    /// </summary>
    public interface IDirectoryRenamer
    {
        /// <summary>
        /// Changes directory name to <paramref name="directoryName" />
        /// </summary>
        /// <param name="directoryName">New directory name.</param>
        void ChangeName(string directoryName);
    }
}
