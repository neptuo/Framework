using Neptuo.Activators;
using Neptuo.FileSystems.Features;
using Neptuo.Models.Features;
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
    public class LocalFileSystem : CollectionFeatureModel, IFileSystem, IFactory<IFileSystemConstant>
    {
        public IDirectory RootDirectory { get; private set; }

        /// <summary>
        /// Creates new instance with <paramref name="rootPath"/> as root directory.
        /// </summary>
        /// <param name="rootPath">Path to root directory.</param>
        /// <param name="isReadOnly">Whether file system should be read-only.</param>
        public LocalFileSystem(string rootPath)
        {
            Ensure.Condition.DirectoryExists(rootPath, "rootPath");

            if (!Path.IsPathRooted(rootPath))
                throw Ensure.Exception.Argument("rootPath", "Path to file system must be rooted.");

            RootDirectory = new LocalDirectory(rootPath);
            this.AddFactory<IFileSystemConstant>(this);
        }

        IFileSystemConstant IFactory<IFileSystemConstant>.Create()
        {
            return new LocalFileSystemConstant();
        }

        
        /// <summary>
        /// Creates static file for <paramref name="filePath"/> of standart file system.
        /// </summary>
        /// <param name="filePath">Path to existing file.</param>
        /// <returns>Static file for <paramref name="filePath"/>.</returns>
        /// <exception cref="FileSystemException">When <paramref name="filePath"/> doesn't point to existing file.</exception>
        public static IFile FromFilePath(string filePath)
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
        public static IDirectory FromDirectoryPath(string directoryPath)
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
