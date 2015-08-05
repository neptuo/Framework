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
    public interface IDirectoryPathSearch
    {
        /// <summary>
        /// Returns enumeration of descendant directories matching <paramref name="directoryNamePath"/>.
        /// </summary>
        /// <param name="directoryNamePath">Name or path for filtering.</param>
        /// <returns>Enumeration of child directories matching <paramref name="directoryNamePath"/>.</returns>
        IEnumerable<IDirectory> FindDirectories(TextSearch directoryNamePath);

        /// <summary>
        /// Returns true if descendant directory matching <paramref name="directoryNamePath"/> is contained.
        /// </summary>
        /// <param name="directoryNamePath">Name or path to test.</param>
        /// <returns>True if directory matching <paramref name="directoryNamePath"/> is contained.</returns>
        bool IsDirectoryContained(TextSearch directoryNamePath);
    }
}
