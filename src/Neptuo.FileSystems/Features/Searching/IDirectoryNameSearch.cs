using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Features.Searching
{
    /// <summary>
    /// Defines contract for searching directories directly in directory.
    /// </summary>
    public interface IDirectoryNameSearch
    {
        /// <summary>
        /// Returns enumeration of child directories matching <paramref name="directoryName"/>.
        /// </summary>
        /// <param name="directoryName">Name for filtering.</param>
        /// <returns>Enumeration of child directories matching <paramref name="directoryName"/>.</returns>
        IEnumerable<IDirectory> FindDirectories(TextSearch directoryName);

        /// <summary>
        /// Returns true if directory with <paramref name="directoryName"/> is contained.
        /// </summary>
        /// <param name="directoryName">Name to test.</param>
        /// <returns>True if directory with <paramref name="directoryName"/> is contained.</returns>
        bool IsDirectoryContained(TextSearch directoryName);
    }
}
