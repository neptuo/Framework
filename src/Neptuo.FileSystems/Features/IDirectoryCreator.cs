using Neptuo.Activators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Features
{
    /// <summary>
    /// New directory creator.
    /// </summary>
    public interface IDirectoryCreator
    {
        /// <summary>
        /// Creates new directory name.
        /// </summary>
        /// <param name="directoryName">New directory name.</param>
        /// <returns>Newly created directory.</returns>
        IDirectory Create(string directoryName);

        /// <summary>
        /// Returns <c>true</c> if <paramref name="directoryName"/> is directory name.
        /// </summary>
        /// <param name="directoryName">File name (without extension).</param>
        /// <returns><c>true</c> if <paramref name="directoryName"/> is directory name; <c>false</c> otherwise.</returns>
        bool IsValidName(string directoryName);
    }
}
