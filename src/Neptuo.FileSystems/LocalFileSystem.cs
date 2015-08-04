using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems
{
    /// <summary>
    /// Virtual file system implemented as stadart file system.
    /// </summary>
    public class LocalFileSystem : IFileSystem
    {
        private readonly LocalDirectory rootDirectory;

        /// <summary>
        /// File system root directory.
        /// </summary>
        public IReadOnlyDirectory RootDirectory
        {
            get { return rootDirectory; }
        }

        public bool IsReadOnly { get; private set; }

        /// <summary>
        /// Creates new instance with <paramref name="rootPath"/> as root directory.
        /// </summary>
        /// <param name="rootPath">Path to root directory.</param>
        /// <param name="isReadOnly">Whether file system should be read-only.</param>
        public LocalFileSystem(string rootPath, bool isReadOnly)
        {
            if (!Path.IsPathRooted(rootPath))
                throw Ensure.Exception.Argument("rootPath", "Path to file system must be rooted.");

            rootDirectory = new LocalDirectory(rootPath);
            IsReadOnly = isReadOnly;
        }

        public bool IsWriteable(IDirectory directory)
        {
            return IsReadOnly;
        }

        public IDirectory AsWriteable(IReadOnlyDirectory directory)
        {
            Ensure.NotNull(directory, "directory");

            if (!IsReadOnly)
                throw Ensure.Exception.FileSystem("File system rooted by '{0}' is read only.", rootDirectory.FullPath);

            LocalDirectory staticDirectory = directory as LocalDirectory;
            if (staticDirectory == null)
            {
                throw Ensure.Exception.FileSystem(
                    "Passed instance of '{0}' into static file system. Static file system operates only on directories of type '{1}'.", 
                    directory.GetType().FullName, 
                    typeof(LocalDirectory).FullName
                );
            }

            return staticDirectory;
        }

        /// <summary>
        /// Creates static file for <paramref name="filePath"/> of standart file system.
        /// </summary>
        /// <param name="filePath">Path to existing file.</param>
        /// <returns>Static file for <paramref name="filePath"/>.</returns>
        /// <exception cref="FileSystemException">When <paramref name="filePath"/> doesn't point to existing file.</exception>
        public static IReadOnlyFile FromFilePath(string filePath)
        {
            Ensure.NotNullOrEmpty(filePath, "filePath");

            if (!File.Exists(filePath))
                throw Ensure.Exception.ArgumentFileNotExist(filePath, "filePath");

            if (!Path.IsPathRooted(filePath))
                filePath = Path.Combine(Environment.CurrentDirectory, filePath);

            return new LocalFile(filePath);
        }

        /// <summary>
        /// Creates static file for <paramref name="directoryPath"/> of standart file system.
        /// </summary>
        /// <param name="directoryPath">Path to existing directory.</param>
        /// <returns>Static file for <paramref name="directoryPath"/>.</returns>
        /// <exception cref="FileSystemException">When <paramref name="directoryPath"/> doesn't point to existing directory.</exception>
        public static IReadOnlyDirectory FromDirectoryPath(string directoryPath)
        {
            Ensure.NotNullOrEmpty(directoryPath, "directoryPath");

            if (!Directory.Exists(directoryPath))
                throw Ensure.Exception.ArgumentDirectoryNotExist(directoryPath, "directoryPath");

            if (!Path.IsPathRooted(directoryPath))
                directoryPath = Path.Combine(Environment.CurrentDirectory, directoryPath);

            return new LocalDirectory(directoryPath);
        }
    }
}
