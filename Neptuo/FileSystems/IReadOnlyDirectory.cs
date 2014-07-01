using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems
{
    /// <summary>
    /// Represents (not updateable) directory in virtual file system.
    /// </summary>
    public interface IReadOnlyDirectory
    {
        /// <summary>
        /// Directory name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Path from root containing this directory name.
        /// </summary>
        string FullPath { get; }

        /// <summary>
        /// Link to parent directory.
        /// </summary>
        IDirectory Parent { get; }

        /// <summary>
        /// Returns enumeration of all child directories.
        /// </summary>
        /// <returns>Enumeration of all child directories.</returns>
        IEnumerable<IDirectory> EnumerateDirectories();

        /// <summary>
        /// Returns enumeration of all child directories.
        /// </summary>
        /// <param name="searchPattern">Name for filtering.</param>
        /// <param name="inAllDescendants">True for not only direct childs.</param>
        /// <returns>Enumeration of all child directories.</returns>
        IEnumerable<IDirectory> FindDirectories(string searchPattern, bool inAllDescendants);

        /// <summary>
        /// Returns enumeration of all child files.
        /// </summary>
        /// <returns>Enumeration of all child files</returns>
        IEnumerable<IFile> EnumerateFiles();

        /// <summary>
        /// Returns enumeration of all child files.
        /// </summary>
        /// <param name="searchPattern">Name for filtering.</param>
        /// <param name="inAllDescendants">True for not only direct childs.</param>
        /// <returns>Enumeration of all child files.</returns>
        IEnumerable<IFile> FindFiles(string searchPattern, bool inAllDescendants);


        /// <summary>
        /// Returns true if this directory contains directory with <paramref name="directoryName"/>.
        /// </summary>
        /// <param name="directoryName">Directory name to test.</param>
        /// <returns>True if this directory contains directory with <paramref name="directoryName"/>.</returns>
        bool ContainsDirectoryName(string directoryName);

        /// <summary>
        /// Returns true if this directory contains file with <paramref name="fileName"/>.
        /// </summary>
        /// <param name="fileName">File name to test.</param>
        /// <returns>True if this directory contains file with <paramref name="fileName"/>.</returns>
        bool ContainsFileName(string fileName);
    }
}
