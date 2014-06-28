using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems
{
    /// <summary>
    /// Represents directory in virtual file system.
    /// </summary>
    public interface IDirectory
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


        /// <summary>
        /// Creates new sub directory in this directory.
        /// </summary>
        /// <param name="name">Name of new directory, must be unique.</param>
        /// <returns>Newly create directory.</returns>
        IDirectory CreateDirectory(string name);

        /// <summary>
        /// Creats new file or overrides is <paramref name="fileName"/> exists.
        /// </summary>
        /// <param name="fileName">New file name.</param>
        /// <param name="fileContent">New file content.</param>
        /// <returns>Newly created file.</returns>
        IFile CreateOrRewriteFile(string fileName, Stream fileContent);
    }
}
