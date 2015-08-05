using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Features.Searching
{
    /// <summary>
    /// Defines contract for searching files in file system sub-tree.
    /// </summary>
    public interface IFilePathSearch
    {
        /// <summary>
        /// Returns enumeration of descendant files matching <paramref name="fileNamePath"/>.
        /// </summary>
        /// <param name="fileNamePath">Name or path for filtering.</param>
        /// <param name="fileExtension">Extension for filtering.</param>
        /// <returns>Enumeration of all child files.</returns>
        IEnumerable<IFile> FindFiles(TextSearch fileNamePath, TextSearch fileExtension);

        /// <summary>
        /// Returns true if file with <paramref name="fileNamePath"/> is contained.
        /// </summary>
        /// <param name="fileNamePath">Name or path to test.</param>
        /// <param name="fileExtension">Extension to text.</param>
        /// <returns>True if file with <paramref name="fileNamePath"/> is contained.</returns>
        bool IsFileContained(TextSearch fileNamePath, TextSearch fileExtension);
    }
}
