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
        /// Returns enumeration of descendant files matching <paramref name="filePath"/>.
        /// </summary>
        /// <param name="filePath">Name or path for filtering.</param>
        /// <param name="fileExtension">Extension for filtering.</param>
        /// <returns>Enumeration of all child files.</returns>
        IEnumerable<IFile> FindFiles(TextSearch filePath, TextSearch fileExtension);

        /// <summary>
        /// Returns true if descendant file matching <paramref name="filePath"/> is contained.
        /// </summary>
        /// <param name="filePath">Name or path to test.</param>
        /// <param name="fileExtension">Extension to text.</param>
        /// <returns>True if descendant file matching <paramref name="filePath"/> is contained.</returns>
        bool IsFileContained(TextSearch filePath, TextSearch fileExtension);
    }
}
