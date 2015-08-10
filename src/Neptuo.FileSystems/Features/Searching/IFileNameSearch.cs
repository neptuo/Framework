using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Features.Searching
{
    /// <summary>
    /// Defines contract for searching files directly in directory.
    /// </summary>
    public interface IFileNameSearch
    {
        /// <summary>
        /// Returns enumeration of child files matching <paramref name="fileName"/> and <paramref name="fileExtension"/>.
        /// </summary>
        /// <param name="fileName">Name for filtering.</param>
        /// <param name="fileExtension">Extension for filtering.</param>
        /// <returns>Enumeration of child files matching <paramref name="fileName"/> and <paramref name="fileExtension"/>.</returns>
        IEnumerable<IFile> FindFiles(TextSearch fileName, TextSearch fileExtension);

        /// <summary>
        /// Returns true if file with <paramref name="fileName"/> and <paramref name="fileExtension"/> is contained.
        /// </summary>
        /// <param name="fileName">Name to test.</param>
        /// <param name="fileExtension">Extension to text.</param>
        /// <returns>True if file with <paramref name="fileName"/> and <paramref name="fileExtension"/> is contained.</returns>
        bool IsFileContained(TextSearch fileName, TextSearch fileExtension);
    }
}
